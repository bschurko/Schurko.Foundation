// Decompiled with JetBrains decompiler
// Type: PNI.Caching.Memory.QueryableMemoryCacheProvider
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Composition;
using System.Linq;


#nullable enable
namespace PNI.Caching.Memory
{
  [ExportMetadata("CacheType", "Memory")]
  public class QueryableMemoryCacheProvider : IQueryableCacheProvider
  {
    private static readonly ConcurrentDictionary<string, MemoryCacheItem> _dictionary = new ConcurrentDictionary<string, MemoryCacheItem>();
    private static readonly QueryableMemoryCacheProvider _instance = new QueryableMemoryCacheProvider();
    private readonly TimeSpan _defaultExpiryTime = new TimeSpan(24, 0, 0);

    private QueryableMemoryCacheProvider()
    {
    }

    public static QueryableMemoryCacheProvider Instance => QueryableMemoryCacheProvider._instance;

    public event QueryableMemoryCacheProvider.CacheChangeHandler OnCacheItemExpired;

    public IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query, TimeSpan cacheDuration)
    {
      string key1 = QueryableMemoryCacheProvider.GetKey<T>(query);
      MemoryCacheItemExpiry expiry = new MemoryCacheItemExpiry()
      {
        AbsoluteExpiryTime = DateTime.Now.Add(cacheDuration)
      };
      MemoryCacheItem memoryCacheItem = QueryableMemoryCacheProvider._dictionary.GetOrAdd(key1, (Func<string, MemoryCacheItem>) (keyToFind => new MemoryCacheItem()
      {
        Item = (object) ((IEnumerable<T>) query).ToList<T>(),
        Expiry = expiry
      }));
      if (DateTime.Now > memoryCacheItem.Expiry.AbsoluteExpiryTime)
      {
        if (this.OnCacheItemExpired != null)
          this.OnCacheItemExpired((object) this, memoryCacheItem);
        ConcurrentDictionary<string, MemoryCacheItem> dictionary = QueryableMemoryCacheProvider._dictionary;
        string key2 = key1;
        MemoryCacheItem addValue = new MemoryCacheItem();
        addValue.Item = memoryCacheItem.Item;
        addValue.Expiry = expiry;
        Func<string, MemoryCacheItem, MemoryCacheItem> updateValueFactory = (Func<string, MemoryCacheItem, MemoryCacheItem>) ((keyToFind, oldItem) => new MemoryCacheItem()
        {
          Item = (object) ((IEnumerable<T>) query).ToList<T>(),
          Expiry = expiry
        });
        memoryCacheItem = dictionary.AddOrUpdate(key2, addValue, updateValueFactory);
      }
      return (IEnumerable<T>) memoryCacheItem.Item;
    }

    public IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query) => this.GetOrCreateCache<T>(query, this._defaultExpiryTime);

    public bool RemoveFromCache<T>(IQueryable<T> query)
    {
      string key = QueryableMemoryCacheProvider.GetKey<T>(query);
      return QueryableMemoryCacheProvider._dictionary.TryRemove(key, out MemoryCacheItem _);
    }

    private static string GetKey<T>(IQueryable<T> query) => query.ToString() + "\n\r" + typeof (T).AssemblyQualifiedName;

    public delegate void CacheChangeHandler(object sender, MemoryCacheItem item);
  }
}
