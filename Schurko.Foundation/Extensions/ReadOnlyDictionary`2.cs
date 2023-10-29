// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.ReadOnlyDictionary`2
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
  public class ReadOnlyDictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
  {
    private readonly IDictionary<TKey, TValue> _dictionary;

    public ReadOnlyDictionary(IDictionary<TKey, TValue> sourceDictionary) => this._dictionary = sourceDictionary;

    public void Add(TKey key, TValue value) => throw new InvalidOperationException("Readonly dictionary");

    public bool ContainsKey(TKey key) => this._dictionary.ContainsKey(key);

    public ICollection<TKey> Keys => (ICollection<TKey>) new ReadOnlyCollection<TKey>((IList<TKey>) new List<TKey>((IEnumerable<TKey>) this._dictionary.Keys));

    public bool Remove(TKey key) => throw new InvalidOperationException("Readonly dictionary");

    public bool TryGetValue(TKey key, out TValue value) => this._dictionary.TryGetValue(key, out value);

    public ICollection<TValue> Values => (ICollection<TValue>) new ReadOnlyCollection<TValue>((IList<TValue>) new List<TValue>((IEnumerable<TValue>) this._dictionary.Values));

    public TValue this[TKey key]
    {
      get => this._dictionary[key];
      set => throw new InvalidOperationException("Readonly dictionary");
    }

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      get => this[key];
      set => throw new InvalidOperationException("Readonly dictionary");
    }

    public void Add(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException("Readonly dictionary");

    public void Clear() => throw new InvalidOperationException("Readonly dictionary");

    public bool Contains(KeyValuePair<TKey, TValue> item) => this._dictionary.Contains(item);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => this._dictionary.CopyTo(array, arrayIndex);

    public int Count => this._dictionary.Count<KeyValuePair<TKey, TValue>>();

    public bool IsReadOnly => true;

    public bool Remove(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException("Readonly dictionary");

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this._dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
