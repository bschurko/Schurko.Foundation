// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.EnumerableExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


#nullable enable
namespace PNI.Extensions
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, T item) => collection.Concat<T>((IEnumerable<T>) new T[1]
    {
      item
    });

    public static IEnumerable<Type> GetTypes(this IEnumerable<object> objIEnumerable) => objIEnumerable.Select<object, Type>((Func<object, Type>) (item => item.GetType()));

    public static bool ContainsType<T>(this IEnumerable<object> objIEnumerable) => objIEnumerable.OfType<T>().Any<T>();

    public static IEnumerable<TX> GetByType<T, TX>(this T[] objArray) where TX : class => objArray.OfType<TX>().Select<TX, TX>((Func<TX, TX>) (item => item));

    public static IEnumerable<TX> GetByType<TX>(this IEnumerable objArray) where TX : class => objArray.OfType<TX>().Select<TX, TX>((Func<TX, TX>) (item => item));

    public static T SelectItem<T>(this IEnumerable<T> items, int? position) => EnumerableExtensions.SelectItem<T>(items.ToList<T>(), position);

    public static T SelectItem<T>(this IList<T> items, int? position)
    {
      position = new int?(position.GetValueOrDefault());
      int count = items.Count;
      int? nullable1 = position;
      int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
      int valueOrDefault = nullable2.GetValueOrDefault();
      return !(count >= valueOrDefault & nullable2.HasValue) ? default (T) : items[position.Value];
    }

    public static T SelectFirstItem<T>(this IEnumerable<T> items) => items != null ? items.SelectItem<T>(new int?()) : default (T);

    public static T SelectFirstItem<T>(this IList<T> items) => items != null ? EnumerableExtensions.SelectItem<T>(items, new int?()) : default (T);

    public static string ToHexString(this byte[] byteArray) => BitConverter.ToString(byteArray);

    public static IList ReadOnly(this IList sourceList) => (IList) new ReadOnlyCollection<object>((IList<object>) sourceList.Cast<object>().ToList<object>());

    public static IList<T> ReadOnly<T>(this IList<T> sourceList) => (IList<T>) new ReadOnlyCollection<T>(sourceList);
  }
}
