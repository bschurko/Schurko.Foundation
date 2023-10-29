// Decompiled with JetBrains decompiler
// Type: PNI.Caching.SlidingExpiration
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace PNI.Caching
{
  public class SlidingExpiration
  {
    private readonly CacheExpiration _currentExpiration;

    public SlidingExpiration(CacheExpiration currentExpiration) => this._currentExpiration = currentExpiration ?? new CacheExpiration();

    public CacheExpiration None() => this.FromTimeSpan(TimeSpan.MaxValue);

    public CacheExpiration AddDays(double days) => this.FromTimeSpan(TimeSpan.FromDays(days));

    public CacheExpiration AddHours(double hours) => this.FromTimeSpan(TimeSpan.FromHours(hours));

    public CacheExpiration AddMinutes(double minutes) => this.FromTimeSpan(TimeSpan.FromMinutes(minutes));

    public CacheExpiration AddSeconds(double seconds) => this.FromTimeSpan(TimeSpan.FromSeconds(seconds));

    public CacheExpiration AddMilliseconds(double milliseconds) => this.FromTimeSpan(TimeSpan.FromMilliseconds(milliseconds));

    public CacheExpiration FromTimeSpan(TimeSpan timeSpan)
    {
      this._currentExpiration.SlidingInterval = this._currentExpiration.SlidingInterval.Add(timeSpan);
      this._currentExpiration.UseDefaultSlidingInterval = false;
      return this._currentExpiration;
    }
  }
}
