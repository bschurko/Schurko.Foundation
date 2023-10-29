// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.WeekNum
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;

namespace PNI.Extensions
{
  public struct WeekNum : IComparable<WeekNum>
  {
    public int Week { get; set; }

    public int Year { get; set; }

    public WeekNum(int week, int year)
      : this()
    {
      this.Week = week;
      this.Year = year;
    }

    public int CompareTo(WeekNum other)
    {
      if (other.Week == this.Week && other.Year == this.Year)
        return 0;
      return other.Year == this.Year ? (other.Week >= this.Week ? 1 : -1) : (other.Year >= this.Year ? 1 : -1);
    }
  }
}
