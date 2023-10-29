// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.EnumeratorExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Collections;
using System.Collections.Generic;


#nullable enable
namespace PNI.Extensions
{
  public static class EnumeratorExtensions
  {
    public static IEnumerator<T> ConvertTo<T>(this IEnumerator enumerator) => (IEnumerator<T>) new EnumeratorWrapper<T>(enumerator);
  }
}
