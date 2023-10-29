// Decompiled with JetBrains decompiler
// Type: PNI.Caching.Memory.MemoryCacheStatistics
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Threading;

namespace PNI.Caching.Memory
{
  public class MemoryCacheStatistics : ICacheStatistics
  {
    private DateTime _startDate;
    private long _items;
    private long _hits;
    private long _misses;
    private long _flushes;

    public DateTime StartDate => this._startDate;

    public long Items => Interlocked.Read(ref this._items);

    public long Hits => Interlocked.Read(ref this._hits);

    public long Misses => Interlocked.Read(ref this._misses);

    public long Flushes => Interlocked.Read(ref this._flushes);

    public void Reset()
    {
      this._startDate = DateTime.UtcNow;
      Interlocked.Exchange(ref this._hits, 0L);
      Interlocked.Exchange(ref this._misses, 0L);
      Interlocked.Exchange(ref this._flushes, 0L);
    }

    public MemoryCacheStatistics() => this._startDate = DateTime.UtcNow;

    public void SetItemCount(long count) => Interlocked.Exchange(ref this._items, count);

    public void AddItem() => Interlocked.Increment(ref this._items);

    public void RemoveItem() => Interlocked.Decrement(ref this._items);

    public void Hit() => Interlocked.Increment(ref this._hits);

    public void Miss() => Interlocked.Increment(ref this._misses);

    public void Flush() => Interlocked.Increment(ref this._flushes);
  }
}
