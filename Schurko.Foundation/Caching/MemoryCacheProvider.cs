// Decompiled with JetBrains decompiler
// Type: PNI.Caching.MemoryCacheProvider
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using Microsoft.Extensions.Logging;
using PNI.Caching.Memory;
using Schurko.Foundation.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Composition;
using System.Linq;


#nullable enable
namespace PNI.Caching
{
  [ExportMetadata("CacheType", "Memory")]
  public class MemoryCacheProvider : ICacheProvider
  {
    private static readonly ConcurrentDictionary<string, MemoryCacheProvider> _instances = new ConcurrentDictionary<string, MemoryCacheProvider>();
    private readonly ConcurrentDictionary<string, MemoryCacheItem> _dictionary = new ConcurrentDictionary<string, MemoryCacheItem>();
    private readonly Dictionary<string, IList<string>> _tagDictionary = new Dictionary<string, IList<string>>();
    private readonly TimeSpan _defaultAbsoluteExpiryInterval = new TimeSpan(24, 0, 0);
    private readonly TimeSpan _defaultSlidingExpiryInterval = TimeSpan.MaxValue;
    private readonly MemoryCacheStatistics _cacheStatistics = new MemoryCacheStatistics();

    public static MemoryCacheProvider GetInstance(string cacheName = null) => MemoryCacheProvider._instances.GetOrAdd(cacheName ?? string.Empty, (Func<string, MemoryCacheProvider>) (s => new MemoryCacheProvider(cacheName)));

    private MemoryCacheProvider(string cacheName = null) => this.CacheName = cacheName;

    public string CacheName { get; set; }

    public string CacheDescription { get; set; }

    public string CacheProviderDescription => "Provider which will cache objects in Memory.  This provider does not require the objects to be serializable";

    public ICacheStatistics Statistics => (ICacheStatistics) this._cacheStatistics;

    public CacheProviderStatus ProviderStatus => CacheProviderStatus.Ready;

    public TObject Add<TObject>(
      string key,
      TObject item,
      CacheExpiration expiration = null,
      IEnumerable<string> tags = null)
    {
      MemoryCacheItemExpiry expiryInfo = this.CalculateExpirationDetails(expiration);
      if (!(tags is string[] strArray))
        strArray = tags == null ? (string[]) null : tags.ToArray<string>();
      string[] tagArray = strArray;
      this.ClearTags(key);
      ConcurrentDictionary<string, MemoryCacheItem> dictionary = this._dictionary;
      string key1 = key;
      MemoryCacheItem addValue = new MemoryCacheItem();
      addValue.Item = (object) item;
      addValue.Expiry = expiryInfo;
      addValue.Tags = (IEnumerable<string>) tagArray;
      Func<string, MemoryCacheItem, MemoryCacheItem> updateValueFactory = (Func<string, MemoryCacheItem, MemoryCacheItem>) ((keyToFind, oldItem) => new MemoryCacheItem()
      {
        Item = (object) (TObject) item,
        Expiry = expiryInfo,
        Tags = (IEnumerable<string>) tagArray
      });
      MemoryCacheItem memoryCacheItem = dictionary.AddOrUpdate(key1, addValue, updateValueFactory);
      this.UpdateTags(key, (IEnumerable<string>) tagArray);
      this._cacheStatistics.SetItemCount((long) this._dictionary.Count);
      return (TObject) memoryCacheItem.Item;
    }

    public object Get(string key)
    {
      MemoryCacheItem memoryCacheItem;
      if (this._dictionary.TryGetValue(key, out memoryCacheItem))
      {
        if (DateTime.Now > memoryCacheItem.Expiry.AbsoluteExpiryTime || DateTime.Now > memoryCacheItem.Expiry.SlidingExpiryTime)
        {
          this._dictionary.TryRemove(key, out memoryCacheItem);
          this._cacheStatistics.Miss();
          return (object) null;
        }
        this._cacheStatistics.Hit();
        return memoryCacheItem.Item;
      }
      this._cacheStatistics.Miss();
      return (object) null;
    }

    public IEnumerable<KeyValuePair<string, object>> GetByTag(string tag) => this._tagDictionary.Where<KeyValuePair<string, IList<string>>>((Func<KeyValuePair<string, IList<string>>, bool>) (kvp => tag == kvp.Key)).SelectMany<KeyValuePair<string, IList<string>>, string, string>((Func<KeyValuePair<string, IList<string>>, IEnumerable<string>>) (item => (IEnumerable<string>) item.Value), (Func<KeyValuePair<string, IList<string>>, string, string>) ((item, key) => key)).Distinct<string>().Select<string, KeyValuePair<string, object>>((Func<string, KeyValuePair<string, object>>) (key => new KeyValuePair<string, object>(key, this.Get(key))));

    public IEnumerable<KeyValuePair<string, object>> GetByAnyTag(IEnumerable<string> tags) => tags == null ? Enumerable.Empty<KeyValuePair<string, object>>() : this._tagDictionary.Where<KeyValuePair<string, IList<string>>>((Func<KeyValuePair<string, IList<string>>, bool>) (kvp => tags.Any<string>((Func<string, bool>) (tag => tag == kvp.Key)))).SelectMany<KeyValuePair<string, IList<string>>, string, string>((Func<KeyValuePair<string, IList<string>>, IEnumerable<string>>) (item => (IEnumerable<string>) item.Value), (Func<KeyValuePair<string, IList<string>>, string, string>) ((item, key) => key)).Distinct<string>().Select<string, KeyValuePair<string, object>>((Func<string, KeyValuePair<string, object>>) (key => new KeyValuePair<string, object>(key, this.Get(key))));

    public IEnumerable<KeyValuePair<string, object>> GetByAllTags(IEnumerable<string> tags)
    {
      if (tags == null)
        return Enumerable.Empty<KeyValuePair<string, object>>();
      if (!(tags is string[] strArray))
        strArray = tags.ToArray<string>();
      string[] tagArray = strArray;
      return this._dictionary.Where<KeyValuePair<string, MemoryCacheItem>>((Func<KeyValuePair<string, MemoryCacheItem>, bool>) (kvp => kvp.Value.Tags != null && kvp.Value.Tags.Count<string>() == ((IEnumerable<string>) tagArray).Count<string>() && kvp.Value.Tags.All<string>((Func<string, bool>) (t => ((IEnumerable<string>) tagArray).Any<string>((Func<string, bool>) (tag => tag == t)))))).Select<KeyValuePair<string, MemoryCacheItem>, KeyValuePair<string, object>>((Func<KeyValuePair<string, MemoryCacheItem>, KeyValuePair<string, object>>) (kvp => new KeyValuePair<string, object>(kvp.Key, this.Get(kvp.Key))));
    }

    public TObject GetOrAdd<TObject>(
      string key,
      Func<TObject> getItem,
      CacheExpiration expiration = null,
      IEnumerable<string> tags = null)
    {
      MemoryCacheItemExpiry expiryInfo = this.CalculateExpirationDetails(expiration);
      if (!(tags is string[] strArray))
        strArray = tags == null ? (string[]) null : tags.ToArray<string>();
      string[] tagArray = strArray;
      bool missed = false;
      MemoryCacheItem memoryCacheItem = this._dictionary.GetOrAdd(key, (Func<string, MemoryCacheItem>) (keyToFind => new MemoryCacheItem()
      {
        Item = (object) this.InjectMiss<TObject>(getItem, out missed),
        Expiry = expiryInfo,
        Tags = (IEnumerable<string>) tagArray
      }));
      if (DateTime.Now > memoryCacheItem.Expiry.AbsoluteExpiryTime || DateTime.Now > memoryCacheItem.Expiry.SlidingExpiryTime)
      {
        this.ClearTags(key);
        ConcurrentDictionary<string, MemoryCacheItem> dictionary = this._dictionary;
        string key1 = key;
        MemoryCacheItem addValue = new MemoryCacheItem();
        addValue.Item = (object) this.InjectMiss<TObject>(getItem, out missed);
        addValue.Expiry = expiryInfo;
        addValue.Tags = (IEnumerable<string>) tagArray;
        Func<string, MemoryCacheItem, MemoryCacheItem> updateValueFactory = (Func<string, MemoryCacheItem, MemoryCacheItem>) ((keyToFind, oldItem) => new MemoryCacheItem()
        {
          Item = (object) this.InjectMiss<TObject>(getItem, out missed),
          Expiry = expiryInfo,
          Tags = (IEnumerable<string>) tagArray
        });
        memoryCacheItem = dictionary.AddOrUpdate(key1, addValue, updateValueFactory);
      }
      if (!missed)
        this._cacheStatistics.Hit();
      this.UpdateTags(key, (IEnumerable<string>) tagArray);
      this._cacheStatistics.SetItemCount((long) this._dictionary.Count);
      return (TObject) memoryCacheItem.Item;
    }

    public bool Remove(string key)
    {
      MemoryCacheItem memoryCacheItem;
      bool flag = this._dictionary.TryRemove(key, out memoryCacheItem);
      if (memoryCacheItem != null && memoryCacheItem.Tags != null)
      {
        foreach (string tag in memoryCacheItem.Tags)
        {
          if (this._tagDictionary.ContainsKey(tag))
            this._tagDictionary[tag].Remove(key);
          if (this._tagDictionary[tag].Count == 0)
            this._tagDictionary.Remove(tag);
        }
      }
      this._cacheStatistics.SetItemCount((long) this._dictionary.Count);
      return flag;
    }

    public void Clear()
    {
      this._cacheStatistics.Flush();
      this._dictionary.Clear();
      this._tagDictionary.Clear();
      this._cacheStatistics.SetItemCount((long) this._dictionary.Count);
    }

    private void ClearTags(string key)
    {
      if (!this._dictionary.ContainsKey(key) || this._dictionary[key].Tags == null)
        return;
      foreach (string tag in this._dictionary[key].Tags)
      {
        if (this._tagDictionary.ContainsKey(tag))
          this._tagDictionary[tag].Remove(key);
        if (this._tagDictionary[tag].Count == 0)
          this._tagDictionary.Remove(tag);
      }
    }

    private void UpdateTags(string key, IEnumerable<string> tags = null)
    {
      if (tags == null)
        return;
      foreach (string tag in tags)
      {
        if (!this._tagDictionary.ContainsKey(tag))
          this._tagDictionary.Add(tag, (IList<string>) new List<string>());
        if (!this._tagDictionary[tag].Contains(key))
          this._tagDictionary[tag].Add(key);
      }
    }

    private MemoryCacheItemExpiry CalculateExpirationDetails(CacheExpiration expiration)
    {
      expiration = expiration ?? Expiration.Default();
      TimeSpan timeSpan1 = expiration.UseDefaultExpiration ? this._defaultAbsoluteExpiryInterval : expiration.ExpirationInterval;
      DateTime dateTime1 = timeSpan1 == TimeSpan.MaxValue ? DateTime.MaxValue : DateTime.Now.Add(timeSpan1);
      TimeSpan timeSpan2 = expiration.UseDefaultSlidingInterval ? this._defaultSlidingExpiryInterval : expiration.SlidingInterval;
      DateTime dateTime2 = timeSpan2 == TimeSpan.MaxValue ? DateTime.MaxValue : DateTime.Now.Add(timeSpan2);
      return new MemoryCacheItemExpiry()
      {
        AbsoluteExpiryTime = dateTime1,
        SlidingExpiryTime = dateTime2,
        SlidingInterval = timeSpan2
      };
    }

    private T InjectMiss<T>(Func<T> getFunc, out bool missed)
    {
      missed = true;
      this._cacheStatistics.Miss();
      try
      {
        return getFunc();
      }
      catch (Exception ex)
      {
        Log.Logger.LogError("Exception while creating item to cache", ex);
        throw;
      }
    }
  }
}
