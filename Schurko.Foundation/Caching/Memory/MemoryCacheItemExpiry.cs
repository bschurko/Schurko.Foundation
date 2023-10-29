// Decompiled with JetBrains decompiler
// Type: PNI.Caching.Memory.MemoryCacheItemExpiry
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;

namespace Schurko.Foundation.Caching.Memory
{
    public struct MemoryCacheItemExpiry
    {
        public DateTime AbsoluteExpiryTime { get; set; }

        public DateTime SlidingExpiryTime { get; set; }

        public TimeSpan SlidingInterval { get; set; }
    }
}
