// Decompiled with JetBrains decompiler
// Type: PNI.Caching.CacheExpiration
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace PNI.Caching
{
  public class CacheExpiration
  {
    public string Name { get; internal set; }

    public bool UseDefaultSlidingInterval { get; internal set; }

    public TimeSpan SlidingInterval { get; internal set; }

    public bool UseDefaultExpiration { get; internal set; }

    public TimeSpan ExpirationInterval { get; internal set; }

    internal CacheExpiration()
    {
      this.UseDefaultExpiration = true;
      this.UseDefaultSlidingInterval = true;
    }

    public SlidingExpiration Sliding => new SlidingExpiration(this);

    public AbsoluteExpiration Absolute => new AbsoluteExpiration(this);
  }
}
