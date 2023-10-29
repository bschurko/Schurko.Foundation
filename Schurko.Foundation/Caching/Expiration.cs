// Decompiled with JetBrains decompiler
// Type: PNI.Caching.Expiration
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll


#nullable enable
using PNI;

namespace Schurko.Foundation.Caching
{
    public static class Expiration
    {
        public static SlidingExpiration Sliding => new SlidingExpiration(null);

        public static AbsoluteExpiration Absolute => new AbsoluteExpiration(null);

        public static CacheExpiration Default(string name = "") => new CacheExpiration()
        {
            Name = name,
            UseDefaultExpiration = true,
            UseDefaultSlidingInterval = true
        };
    }
}
