using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Kami.Utils;

public static class MathUtil
{
    public static TimeSpan Median(this IEnumerable<TimeSpan> source) => TimeSpan.FromTicks(source.Median(x => x.Ticks));

    public static TNumber Median<TSource, TNumber>(this IEnumerable<TSource> source, Func<TSource, TNumber> selector)
        where TNumber : INumber<TNumber> => source.Select(selector).Median();

    public static T Median<T>(this IEnumerable<T> source) where T : INumber<T>
    {
        var items = source.OrderBy(x => x).ToArray();
        var middleIndex = items.Length / 2;
        if (items.Length % 2 != 0)
            return items[middleIndex];
        return (items[middleIndex] + items[middleIndex - 1]) / (T.One + T.One);
    }

    public static T? MaxOrDefault<T>(this IEnumerable<T> source)
        where T : INumber<T> => BestOrDefault(source, (arg1, arg2) => arg1 > arg2);

    public static T? BestOrDefault<T>(this IEnumerable<T> source, Func<T, T, bool> firstIsBetter) where T : INumber<T>
    {
        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext())
            return default;

        var best = enumerator.Current;
        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            if (firstIsBetter(item, best))
                best = item;
        }

        return best;
    }

    public static T Percentile<T>(this IEnumerable<T> data, double percentile) where T : IComparable<T>
    {
        var sortedData = data.Order().ToArray();
        return GetItem(sortedData, percentile);
    }

    public static T[] Percentiles<T>(this IEnumerable<T> data, params double[] percentiles) where T : IComparable<T>
    {
        if (percentiles.Length == 0)
            throw new ArgumentException("At least one percentile must be specified", nameof(percentiles));

        var sortedData = data.Order().ToArray();
        var result = new T[percentiles.Length];
        for (var i = 0; i < percentiles.Length; i++)
            result[i] = GetItem(sortedData, percentiles[i]);
        return result;
    }

    private static T GetItem<T>(IReadOnlyList<T> sortedData, double percentile)
    {
        if (sortedData.Count == 0)
            throw new ArgumentException("Data contains no elements");
        if (percentile is < 0 or > 100)
            throw new ArgumentException("Percentile must be >= 0 and <= 100");
        var index = (int)Math.Ceiling(sortedData.Count * percentile / 100.0) - 1;
        return sortedData[index];
    }
}