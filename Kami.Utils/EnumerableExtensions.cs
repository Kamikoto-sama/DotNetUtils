using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Utils;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Single<T>(this T value)
    {
        yield return value;
    }

    public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
    {
        while (enumerator.MoveNext())
            yield return enumerator.Current;
    }

    public static IEnumerable<List<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> separate)
    {
        var part = new List<T>();
        foreach (var item in source)
        {
            if (separate(item))
            {
                if (part.Count > 0)
                    yield return part;
                part = new List<T>();
            }
            else
                part.Add(item);
        }

        if (part.Count > 0)
            yield return part;
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var index = 0;
        foreach (var item in source)
            action(item, index++);
    }

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        where TKey : notnull =>
        source.ToDictionary(x => x.Key, x => x.Value);

    public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source)
        where TKey : notnull =>
        source.ToDictionary(x => x.Key, x => x.ToList());

    public static Dictionary<T, int> CountItems<T>(this IEnumerable<T> source) where T : notnull
    {
        var counts = new Dictionary<T, int>();
        foreach (var item in source)
            if (counts.ContainsKey(item))
                counts[item]++;
            else
                counts[item] = 1;

        return counts;
    }

    public static int SequenceGetHashCode<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        using var enumerator = source?.GetEnumerator();
        if (enumerator == null || !enumerator.MoveNext())
            return 0;

        var hash = comparer.GetHashCode(enumerator.Current);
        while (enumerator.MoveNext())
            hash = HashCode.Combine(hash, comparer.GetHashCode(enumerator.Current));

        return hash;
    }

    public static IEnumerable<T> WhereNotDefault<T>(this IEnumerable<T?> source)
    {
        return source.Where(x => x?.Equals(default) == false);
    }
}