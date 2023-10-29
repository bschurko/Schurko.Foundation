// Decompiled with JetBrains decompiler
// Type: PNI.Caching.IQueryableCacheProvider
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections.Generic;
using System.Linq;


#nullable enable
namespace PNI.Caching
{
  public interface IQueryableCacheProvider
  {
    IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query);

    IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query, TimeSpan cacheDuration);

    bool RemoveFromCache<T>(IQueryable<T> query);
  }
}
