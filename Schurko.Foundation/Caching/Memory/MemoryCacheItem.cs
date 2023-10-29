// Decompiled with JetBrains decompiler
// Type: PNI.Caching.Memory.MemoryCacheItem
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Collections.Generic;


#nullable enable
namespace Schurko.Foundation.Caching.Memory
{
    public class MemoryCacheItem
    {
        public object Item { get; set; }

        public MemoryCacheItemExpiry Expiry { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
