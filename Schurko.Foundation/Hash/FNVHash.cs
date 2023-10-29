// Decompiled with JetBrains decompiler
// Type: PNI.Hash.FNVHash
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#nullable enable
namespace PNI.Hash
{
  public sealed class FNVHash
  {
    private const uint FnvPrime32 = 16777619;
    private const ulong FnvPrime64 = 1099511628211;
    private const uint FnvOffset32 = 2166136261;
    private const ulong FnvOffset64 = 14695981039346656037;

    public static uint Hash32(IEnumerable<byte> data)
    {
      uint num1 = 2166136261;
      foreach (byte num2 in data)
      {
        num1 ^= (uint) num2;
        num1 *= 16777619U;
      }
      return num1;
    }

    public static ulong Hash64(IEnumerable<byte> data)
    {
      ulong num1 = 14695981039346656037;
      foreach (byte num2 in data)
      {
        num1 ^= (ulong) num2;
        num1 *= 1099511628211UL;
      }
      return num1;
    }

    public static ulong HashString(params object[] data) => FNVHash.Hash64((IEnumerable<byte>) Encoding.UTF8.GetBytes(((IEnumerable<object>) data).Select<object, string>((Func<object, string>) (e => e.ToString())).OrderBy<string, string>((Func<string, string>) (s => s)).Aggregate<string>((Func<string, string, string>) ((a, b) => string.Format("{0}_{1}", (object) a, (object) b)))));
  }
}
