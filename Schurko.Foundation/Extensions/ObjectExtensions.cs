// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.ObjectExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace PNI.Extensions
{
  public static class ObjectExtensions
  {
    public static IfStatement<TSource> If<TSource>(
      this TSource value,
      Func<TSource, bool> condition,
      Action<TSource> action)
    {
      if (!condition(value))
        return IfStatement.False<TSource>(value);
      action(value);
      return IfStatement.True<TSource>(value);
    }

    public static IfStatement<TSource> ElseIf<TSource>(
      this IfStatement<TSource> statement,
      Func<TSource, bool> condition,
      Action<TSource> action)
    {
      if (statement.State || !condition(statement.Context))
        return statement;
      action(statement.Context);
      return IfStatement.True<TSource>(statement.Context);
    }

    public static void Else<TSource>(this IfStatement<TSource> statement, Action<TSource> action)
    {
      if (statement.State)
        return;
      action(statement.Context);
    }

    //public static IfStatement<TSource> IfNotNullOrDefault<TSource>(
    //  this TSource value,
    //  Action<TSource> operation)
    //  where TSource : IEquatable<TSource>
    //{
    //  if ((object) value != null)
    //  {
    //    ref TSource local = ref value;
    //    if ((object) default (TSource) == null)
    //    {
    //      TSource source = local;
    //      local = ref source;
    //    }
    //    TSource other = default (TSource);
    //    if (!local.Equals(other))
    //    {
    //      operation(value);
    //      return IfStatement.True<TSource>(value);
    //    }
    //  }
    //  return IfStatement.False<TSource>(value);
    //}

    public static IfStatement<TSource> IfNotNull<TSource>(
      this TSource value,
      Action<TSource> operation)
      where TSource : class
    {
      if ((object) value == null)
        return IfStatement.False<TSource>(value);
      operation(value);
      return IfStatement.True<TSource>(value);
    }
  }
}
