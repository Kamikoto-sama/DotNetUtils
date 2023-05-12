using System;
using System.Collections;
using System.Collections.Generic;

namespace Kami.Utils;

public static class CollectionExtensions
{
    public static T? NullIfEmpty<T>(this T? collection) where T : class, ICollection =>
        collection == null || collection.Count == 0 ? null : collection;

    public static List<T> EmptyIfNull<T>(this List<T>? list) => list == null || list.Count == 0 ? new List<T>(0) : list;

    public static T[] EmptyIfNull<T>(this T[]? array) => array == null || array.Length == 0 ? Array.Empty<T>() : array;

    public static Dictionary<TKey, TValue> EmptyIfNull<TKey, TValue>(this Dictionary<TKey, TValue>? dictionary) where TKey : notnull =>
        dictionary == null || dictionary.Count == 0 ? new Dictionary<TKey, TValue>(0) : dictionary;
}