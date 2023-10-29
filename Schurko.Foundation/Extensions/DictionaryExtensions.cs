// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.DictionaryExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


#nullable enable
namespace PNI.Extensions
{
  public static class DictionaryExtensions
  {
    public static IDictionary ReadOnly(this IDictionary sourceDictionary)
    {
      IDictionary<object, object> sourceDictionary1 = (IDictionary<object, object>) new Dictionary<object, object>();
      foreach (DictionaryEntry source in sourceDictionary)
        sourceDictionary1.Add(new KeyValuePair<object, object>(source.Key, source.Value));
      return (IDictionary) new ReadOnlyDictionary<object, object>(sourceDictionary1).ToDictionary<KeyValuePair<object, object>, object, object>((Func<KeyValuePair<object, object>, object>) (k => k.Key), (Func<KeyValuePair<object, object>, object>) (v => v.Value));
    }

    //public static TValue GetValue<TKey, TValue>(
    //  this IDictionary<TKey, TValue> sourceDictionary,
    //  TKey key,
    //  TValue defaultValue = null)
    //{
    //  TValue obj;
    //  return !sourceDictionary.TryGetValue(key, out obj) ? defaultValue : obj;
    //}

    public static IDictionary<TKey, TValue> ReadOnly<TKey, TValue>(
      this IDictionary<TKey, TValue> sourceDictionary)
    {
      return (IDictionary<TKey, TValue>) new ReadOnlyDictionary<TKey, TValue>(sourceDictionary);
    }
  }
}
