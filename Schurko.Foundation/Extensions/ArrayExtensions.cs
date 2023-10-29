// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.ArrayExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace PNI.Extensions
{
  public static class ArrayExtensions
  {
    public static T[] ConcatArray<T>(this T[] arr1, T[] arr2)
    {
      T[] dst = new T[arr1.Length + arr2.Length];
      Buffer.BlockCopy((Array) arr1, 0, (Array) dst, 0, arr1.Length);
      Buffer.BlockCopy((Array) arr2, 0, (Array) dst, arr1.Length, arr2.Length);
      return dst;
    }

    public static T[] SubArray<T>(this T[] arr, int start, int length = 0)
    {
      if (length == 0)
        length = arr.Length - start;
      T[] dst = new T[length];
      Buffer.BlockCopy((Array) arr, start, (Array) dst, 0, length);
      return dst;
    }
  }
}
