// Decompiled with JetBrains decompiler
// Type: PNI.Caching.ICacheStatistics
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;

namespace PNI.Caching
{
  public interface ICacheStatistics
  {
    DateTime StartDate { get; }

    long Items { get; }

    long Hits { get; }

    long Misses { get; }

    long Flushes { get; }

    void Reset();
  }
}
