// Decompiled with JetBrains decompiler
// Type: PNI.Caching.ConfigurableCacheSettings
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll


#nullable enable
namespace PNI.Caching
{
  public class ConfigurableCacheSettings : CacheSettings
  {
    private ConfigurableCacheProviderMode _mode;
    private string _chain;

    internal string CacheName { get; private set; }

    public ConfigurableCacheProviderMode Mode
    {
      get => this._mode;
      set
      {
        if (this.Locked)
          return;
        this._mode = value;
      }
    }

    public string Chain
    {
      get => this._chain;
      set
      {
        if (this.Locked)
          return;
        this._chain = value;
      }
    }

    public ConfigurableCacheSettings(string cacheName = null)
    {
      if (this.Locked)
        return;
      this.CacheName = cacheName;
      this.Mode = CacheSettings.GetCacheSetting<ConfigurableCacheProviderMode>(cacheName, "Configurable.Mode", ConfigurableCacheProviderMode.Fallback);
      this.Chain = CacheSettings.GetCacheSetting<string>(cacheName, "Configurable.Chain", "Memory");
    }
  }
}
