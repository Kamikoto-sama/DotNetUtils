using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Utils;

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

    public static T? MaxOrDefault<T>(this IEnumerable<T> source) where T : INumber<T>
    {
        return BestOrDefault(source, (arg1, arg2) => arg1 > arg2);
    }

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
}