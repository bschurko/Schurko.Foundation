// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.IfStatement
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll


#nullable enable
namespace PNI.Extensions
{
  public class IfStatement
  {
    internal static IfStatement<TSource> True<TSource>(TSource data) => new IfStatement<TSource>(true, data);

    internal static IfStatement<TSource> False<TSource>(TSource data) => new IfStatement<TSource>(false, data);
  }
}
