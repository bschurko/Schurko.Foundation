// Decompiled with JetBrains decompiler
// Type: PNI.Caching.ConfigurableCacheSettings
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll


#nullable enable
using PNI;

namespace Schurko.Foundation.Caching
{
    public class ConfigurableCacheSettings : CacheSettings
    {
        private ConfigurableCacheProviderMode _mode;
        private string _chain;

        internal string CacheName { get; private set; }

        public ConfigurableCacheProviderMode Mode
        {
            get => _mode;
            set
            {
                if (Locked)
                    return;
                _mode = value;
            }
        }

        public string Chain
        {
            get => _chain;
            set
            {
                if (Locked)
                    return;
                _chain = value;
            }
        }

        public ConfigurableCacheSettings(string cacheName = null)
        {
            if (Locked)
                return;
            CacheName = cacheName;
            Mode = GetCacheSetting(cacheName, "Configurable.Mode", ConfigurableCacheProviderMode.Fallback);
            Chain = GetCacheSetting(cacheName, "Configurable.Chain", "Memory");
        }
    }
}
