
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


#nullable enable
namespace Schurko.Foundation.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary ReadOnly(this IDictionary sourceDictionary)
        {
            IDictionary<object, object> sourceDictionary1 = new Dictionary<object, object>();
            foreach (DictionaryEntry source in sourceDictionary)
                sourceDictionary1.Add(new KeyValuePair<object, object>(source.Key, source.Value));
            return new ReadOnlyDictionary<object, object>(sourceDictionary1).ToDictionary(k => k.Key, v => v.Value);
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
            return new ReadOnlyDictionary<TKey, TValue>(sourceDictionary);
        }
    }
}
