// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.IntExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.ComponentModel;


#nullable enable
namespace PNI.Extensions
{
  public static class IntExtensions
  {
    public static bool AsBool(this int value) => value == 0;

    public static int ToInt(this string parseValue, int defaultValue = -1)
    {
      int result;
      return !int.TryParse(parseValue, out result) ? defaultValue : result;
    }

    public static T ToEnum<T>(this int index) => Enum.IsDefined(typeof (T), (object) index) ? (T) Enum.ToObject(typeof (T), index) : throw new InvalidEnumArgumentException(nameof (index), index, typeof (T));

    public static T ToEnum<T>(this int index, T defaultValue)
    {
      try
      {
        return index.ToEnum<T>();
      }
      catch
      {
        return defaultValue;
      }
    }
  }
}
