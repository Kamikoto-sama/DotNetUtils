using System;
using System.Collections.Generic;

namespace Kami.Utils;

public static class TupleUtils
{
    public static void Deconstruct<T>(this IEnumerable<T> source, out T item0, out T item1)
    {
        const int count = 2;
        using var enumerator = source.GetEnumerator();
        item0 = GetNextElement(enumerator, count);
        item1 = GetNextElement(enumerator, count);
    }

    public static void Deconstruct<T>(this IEnumerable<T> source, out T item0, out T item1, out T item2)
    {
        const int count = 3;
        using var enumerator = source.GetEnumerator();
        item0 = GetNextElement(enumerator, count);
        item1 = GetNextElement(enumerator, count);
        item2 = GetNextElement(enumerator, count);
    }

    public static void Deconstruct<T>(this IEnumerable<T> source, out T item0, out T item1, out T item2, out T item3)
    {
        const int count = 4;
        using var enumerator = source.GetEnumerator();
        item0 = GetNextElement(enumerator, count);
        item1 = GetNextElement(enumerator, count);
        item2 = GetNextElement(enumerator, count);
        item3 = GetNextElement(enumerator, count);
    }

    private static T GetNextElement<T>(IEnumerator<T> enumerator, int expectedSize)
    {
        if (!enumerator.MoveNext())
            throw new InvalidOperationException($"Collection expected to have {expectedSize} elements");
        return enumerator.Current;
    }
}