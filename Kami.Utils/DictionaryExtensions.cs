using System;
using System.Collections.Generic;

namespace Kami.Utils;

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
        if (!dictionary.TryGetValue(key, out var list))
            dictionary[key] = list = new List<TValue>();
        list.Add(value);
    }

    public static void AddToList<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, IEnumerable<TValue> values)
    {
        if (!dictionary.TryGetValue(key, out var list))
            dictionary[key] = list = new List<TValue>();
        list.AddRange(values);
    }

    public static bool AddToSet<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dictionary, TKey key, TValue value) where TKey : notnull
    {
        if (!dictionary.TryGetValue(key, out var set))
            dictionary[key] = set = new HashSet<TValue>();
        return set.Add(value);
    }

    public static void AddToSet<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dictionary, TKey key, IEnumerable<TValue> values) where TKey : notnull
    {
        if (!dictionary.TryGetValue(key, out var set))
            dictionary[key] = set = new HashSet<TValue>();
        set.AddRange(values);
    }
}