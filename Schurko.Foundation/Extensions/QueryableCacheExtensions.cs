// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.QueryableCacheExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using PNI.Caching;
using PNI.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;


#nullable enable
namespace PNI.Extensions
{
  public static class QueryableCacheExtensions
  {
    private static IQueryableCacheProvider _cacheProvider;

    public static IQueryableCacheProvider CurrentProvider => QueryableCacheExtensions._cacheProvider;

    static QueryableCacheExtensions() => QueryableCacheExtensions.SetCacheProvider((IQueryableCacheProvider) QueryableMemoryCacheProvider.Instance);

    public static void SetCacheProvider(IQueryableCacheProvider provider) => QueryableCacheExtensions._cacheProvider = provider != null ? provider : throw new ArgumentNullException(nameof (provider), "Provider must not be null.");

    public static IEnumerable<T> AsCacheable<T>(this IQueryable<T> query)
    {
      QueryableCacheExtensions.ValidateCacheProvider();
      return QueryableCacheExtensions._cacheProvider.GetOrCreateCache<T>(query);
    }

    public static IEnumerable<T> AsCacheable<T>(this IQueryable<T> query, TimeSpan cacheDuration)
    {
      QueryableCacheExtensions.ValidateCacheProvider();
      return QueryableCacheExtensions._cacheProvider.GetOrCreateCache<T>(query, cacheDuration);
    }

    public static bool RemoveFromCache<T>(IQueryable<T> query)
    {
      QueryableCacheExtensions.ValidateCacheProvider();
      return QueryableCacheExtensions._cacheProvider.RemoveFromCache<T>(query);
    }

    private static void ValidateCacheProvider()
    {
      if (QueryableCacheExtensions._cacheProvider == null)
        throw new InvalidOperationException("Please set cache provider (call SetCacheProvider) before using caching");
    }
  }
}
