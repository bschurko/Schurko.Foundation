// Decompiled with JetBrains decompiler
// Type: PNI.Caching.ConfigurableCacheProvider
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using PNI.Caching.Memory;
using PNI.Ioc.MEF;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Composition;
using System.Linq;


#nullable enable
namespace PNI.Caching
{
  [ExportMetadata("CacheType", "Configurable")]
  public class ConfigurableCacheProvider : ICacheProvider
  {
    private static readonly ConcurrentDictionary<string, ConfigurableCacheProvider> Instances = new ConcurrentDictionary<string, ConfigurableCacheProvider>();
    private static readonly MemoryCacheStatistics CacheStatistics = new MemoryCacheStatistics();
    private ICacheProvider _handler;
    private ConfigurableCacheProvider _nextProvider;
    private ConfigurableCacheSettings _settings;
    private bool _configured;

    public static ConfigurableCacheProvider GetInstance(string cacheName = null) => ConfigurableCacheProvider.Instances.GetOrAdd(cacheName ?? string.Empty, (Func<string, ConfigurableCacheProvider>) (s => new ConfigurableCacheProvider(cacheName)));

    public ConfigurableCacheProvider() => this._handler = (ICacheProvider) ConfigurableCacheProvider.GetInstance();

    private ConfigurableCacheProvider(string cacheName) => this.CacheName = cacheName;

    private ConfigurableCacheProvider(string cacheName, Queue<string> providerChain)
    {
      this.CacheName = cacheName;
      this.BuildChain(providerChain);
    }

    public ConfigurableCacheSettings Settings
    {
      get
      {
        ConfigurableCacheSettings settings = this._settings ?? (this._settings = new ConfigurableCacheSettings(this.CacheName));
        if (settings.CacheName != this.CacheName)
          settings = this._settings = new ConfigurableCacheSettings(this.CacheName);
        return settings;
      }
    }

    public string CacheName { get; set; }

    public string CacheDescription { get; set; }

    public string CacheProviderDescription => "Provider which will resolve configured Cache Providers to provider either a Fallback cache provider model which will the first provider in the chain that isn't in a faulted state, or a Cascading cache provider modelwhich will use multiple cache providers to try to reduce the need for regenerating the item to add.";

    public ICacheStatistics Statistics => (ICacheStatistics) ConfigurableCacheProvider.CacheStatistics;

    public CacheProviderStatus ProviderStatus => CacheProviderStatus.Ready;

    public TObject Add<TObject>(
      string key,
      TObject item,
      CacheExpiration expiration = null,
      IEnumerable<string> tags = null)
    {
      this.EnsureCacheProviderIsConfigured();
      if (this.Settings.Mode == ConfigurableCacheProviderMode.Fallback)
      {
        TObject @object = default (TObject);
        if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
          @object = this._handler.Add<TObject>(key, item, expiration, tags);
        else if (!this.EndOfChain)
          @object = this._nextProvider.Add<TObject>(key, item, expiration, tags);
        return @object;
      }
      if (this.Settings.Mode != ConfigurableCacheProviderMode.Cascading)
        throw new NotImplementedException(string.Format("Mode {0} has not implemented Add", (object) this.Settings.Mode));
      string[] array = tags == null ? (string[]) null : tags.ToArray<string>();
      TObject object1 = this._handler.Add<TObject>(key, item, expiration, (IEnumerable<string>) array);
      if (!this.EndOfChain)
        object1 = this._nextProvider.Add<TObject>(key, object1, expiration, (IEnumerable<string>) array);
      return object1;
    }

    public object Get(string key)
    {
      this.EnsureCacheProviderIsConfigured();
      if (this.Settings.Mode == ConfigurableCacheProviderMode.Fallback)
      {
        object obj = (object) null;
        if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
          obj = this._handler.Get(key);
        else if (!this.EndOfChain)
          obj = this._nextProvider.Get(key);
        if (obj == null)
          ConfigurableCacheProvider.CacheStatistics.Miss();
        else
          ConfigurableCacheProvider.CacheStatistics.Hit();
        return obj;
      }
      if (this.Settings.Mode != ConfigurableCacheProviderMode.Cascading)
        throw new NotImplementedException(string.Format("Mode {0} has not implemented Get", (object) this.Settings.Mode));
      object obj1 = this._handler.Get(key);
      if (obj1 == null && !this.EndOfChain)
      {
        ConfigurableCacheProvider.CacheStatistics.Miss();
        obj1 = this._nextProvider.Get(key);
        this._handler.Add<object>(key, obj1);
      }
      else
        ConfigurableCacheProvider.CacheStatistics.Miss();
      if (obj1 != null)
        ConfigurableCacheProvider.CacheStatistics.Hit();
      return obj1;
    }

    public IEnumerable<KeyValuePair<string, object>> GetByTag(string tag)
    {
      this.EnsureCacheProviderIsConfigured();
      if (this.Settings.Mode != ConfigurableCacheProviderMode.Fallback)
        throw new NotImplementedException("GetByTag not implemented for Cascading Mode");
      IEnumerable<KeyValuePair<string, object>> byTag = (IEnumerable<KeyValuePair<string, object>>) null;
      if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
        byTag = this._handler.GetByTag(tag);
      else if (!this.EndOfChain)
        byTag = this._nextProvider.GetByTag(tag);
      if (byTag == null)
        ConfigurableCacheProvider.CacheStatistics.Miss();
      else
        ConfigurableCacheProvider.CacheStatistics.Hit();
      return byTag;
    }

    public IEnumerable<KeyValuePair<string, object>> GetByAnyTag(IEnumerable<string> tags)
    {
      this.EnsureCacheProviderIsConfigured();
      if (this.Settings.Mode != ConfigurableCacheProviderMode.Fallback)
        throw new NotImplementedException("GetByAnyTag not implemented for Cascading Mode");
      IEnumerable<KeyValuePair<string, object>> byAnyTag = (IEnumerable<KeyValuePair<string, object>>) null;
      if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
        byAnyTag = this._handler.GetByAnyTag(tags);
      else if (!this.EndOfChain)
        byAnyTag = this._nextProvider.GetByAnyTag(tags);
      if (byAnyTag == null)
        ConfigurableCacheProvider.CacheStatistics.Miss();
      else
        ConfigurableCacheProvider.CacheStatistics.Hit();
      return byAnyTag;
    }

    public IEnumerable<KeyValuePair<string, object>> GetByAllTags(IEnumerable<string> tags)
    {
      this.EnsureCacheProviderIsConfigured();
      if (this.Settings.Mode != ConfigurableCacheProviderMode.Fallback)
        throw new NotImplementedException("GetByAnyTag not implemented for Cascading Mode");
      IEnumerable<KeyValuePair<string, object>> byAllTags = (IEnumerable<KeyValuePair<string, object>>) null;
      if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
        byAllTags = this._handler.GetByAllTags(tags);
      else if (!this.EndOfChain)
        byAllTags = this._nextProvider.GetByAllTags(tags);
      if (byAllTags == null)
        ConfigurableCacheProvider.CacheStatistics.Miss();
      else
        ConfigurableCacheProvider.CacheStatistics.Hit();
      return byAllTags;
    }

    public TObject GetOrAdd<TObject>(
      string key,
      Func<TObject> getItem,
      CacheExpiration expiration = null,
      IEnumerable<string> tags = null)
    {
      this.EnsureCacheProviderIsConfigured();
      bool missed = false;
      if (this.Settings.Mode == ConfigurableCacheProviderMode.Fallback)
      {
        TObject orAdd = default (TObject);
        if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
        {
          orAdd = this._handler.GetOrAdd<TObject>(key, (Func<TObject>) (() => ConfigurableCacheProvider.InjectMiss<TObject>(getItem, out missed)), expiration, tags);
          if (!missed)
            ConfigurableCacheProvider.CacheStatistics.Hit();
        }
        else if (!this.EndOfChain)
          orAdd = this._nextProvider.GetOrAdd<TObject>(key, getItem, expiration, tags);
        return orAdd;
      }
      if (this.Settings.Mode != ConfigurableCacheProviderMode.Cascading)
        throw new NotImplementedException(string.Format("Mode {0} has not implemented GetOrAdd", (object) this.Settings.Mode));
      TObject orAdd1 = this._handler.GetOrAdd<TObject>(key, (Func<TObject>) (() => this.EndOfChain ? ConfigurableCacheProvider.InjectMiss<TObject>(getItem, out missed) : this._nextProvider.GetOrAdd<TObject>(key, getItem, expiration, tags)), expiration, tags);
      if (missed)
        return orAdd1;
      ConfigurableCacheProvider.CacheStatistics.Hit();
      return orAdd1;
    }

    public bool Remove(string key)
    {
      this.EnsureCacheProviderIsConfigured();
      if (this.Settings.Mode == ConfigurableCacheProviderMode.Fallback)
      {
        if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
          return this._handler.Remove(key);
        if (!this.EndOfChain)
          return this._nextProvider.Remove(key);
      }
      if (this.Settings.Mode != ConfigurableCacheProviderMode.Cascading)
        throw new NotImplementedException(string.Format("Mode {0} has not implemented Remove", (object) this.Settings.Mode));
      bool flag = this._handler.Remove(key);
      if (!this.EndOfChain)
        flag ^= this._nextProvider.Remove(key);
      return flag;
    }

    public void Clear()
    {
      this.EnsureCacheProviderIsConfigured();
      ConfigurableCacheProvider.CacheStatistics.Flush();
      if (this.Settings.Mode == ConfigurableCacheProviderMode.Fallback)
      {
        if (this._handler.ProviderStatus == CacheProviderStatus.Ready)
        {
          this._handler.Clear();
        }
        else
        {
          if (this.EndOfChain)
            return;
          this._nextProvider.Clear();
        }
      }
      else
      {
        if (this.Settings.Mode != ConfigurableCacheProviderMode.Cascading)
          throw new NotImplementedException(string.Format("Mode {0} has not implemented Clear", (object) this.Settings.Mode));
        this._handler.Clear();
        if (this.EndOfChain)
          return;
        this._nextProvider.Clear();
      }
    }

    private void EnsureCacheProviderIsConfigured()
    {
      if (this._configured)
        return;
      this.Settings.Lock();
      this.BuildChain(new Queue<string>(((IEnumerable<string>) this.Settings.Chain.Split(new char[2]
      {
        ',',
        ';'
      })).Select<string, string>((Func<string, string>) (c => c.Trim()))));
      this._configured = true;
    }

    private void BuildChain(Queue<string> providerChain)
    {
      while (providerChain.Any<string>())
      {
                Func<Lazy<ICacheProvider, IDictionary<string, object>>, bool> provider = null;

        string str = providerChain.Dequeue();
        ICacheProvider cacheProvider = str == "Memory" ? (ICacheProvider) MemoryCacheProvider.GetInstance(this.CacheName)
                    : DependencyInjector.AllWithMetaData<ICacheProvider>()
                    .Filter("CacheType", str).Resolve<ICacheProvider>();

        if (cacheProvider != null)
        {
          cacheProvider.CacheName = this.CacheName;
          this._handler = cacheProvider;
          this._nextProvider = new ConfigurableCacheProvider(this.CacheName, providerChain)
          {
            _configured = true
          };
        }
      }
      if (this._handler != null)
        return;
      this._handler = (ICacheProvider) MemoryCacheProvider.GetInstance(this.CacheName);
    }

    private static T InjectMiss<T>(Func<T> getFunc, out bool missed)
    {
      missed = true;
      ConfigurableCacheProvider.CacheStatistics.Miss();
      return getFunc();
    }

    private bool EndOfChain => this._nextProvider == null || this._nextProvider._handler == null;
  }
}
