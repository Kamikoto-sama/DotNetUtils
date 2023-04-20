using System;
using System.Collections.Generic;

namespace Utils;

public static class DictionaryExtensions
{
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue, Func<TValue, TValue> update)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = update(dictionary[key]);
        else
            dictionary[key] = defaultValue;
    }

    public static void AddToList<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
    {
        if (!dictionary.ContainsKey(key))
            dictionary[key] = new List<TValue>();
        dictionary[key].Add(value);
    }
}