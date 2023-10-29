// Decompiled with JetBrains decompiler
// Type: PNI.Caching.AbsoluteExpiration
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace PNI.Caching
{
  public class AbsoluteExpiration
  {
    private readonly CacheExpiration _currentExpiration;

    public AbsoluteExpiration(CacheExpiration currentExpiration) => this._currentExpiration = currentExpiration ?? new CacheExpiration();

    public CacheExpiration None() => this.FromTimeSpan(TimeSpan.MaxValue);

    public CacheExpiration AddDays(double days) => this.FromTimeSpan(TimeSpan.FromDays(days));

    public CacheExpiration AddHours(double hours) => this.FromTimeSpan(TimeSpan.FromHours(hours));

    public CacheExpiration AddMinutes(double minutes) => this.FromTimeSpan(TimeSpan.FromMinutes(minutes));

    public CacheExpiration AddSeconds(double seconds) => this.FromTimeSpan(TimeSpan.FromSeconds(seconds));

    public CacheExpiration AddMilliseconds(double milliseconds) => this.FromTimeSpan(TimeSpan.FromMilliseconds(milliseconds));

    public CacheExpiration FromTimeSpan(TimeSpan timeSpan)
    {
      this._currentExpiration.ExpirationInterval = timeSpan;
      this._currentExpiration.UseDefaultExpiration = false;
      return this._currentExpiration;
    }
  }
}
