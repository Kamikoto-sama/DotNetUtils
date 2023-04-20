using System;
using System.Collections.Generic;

namespace Utils;

public static class TupleUtils
{
    public static void Deconstruct<T>(this IEnumerable<T> source, out T item0, out T item1)
    {
        using var enumerator = source.GetEnumerator();
        item0 = GetNextElement(enumerator, 2);
        item1 = GetNextElement(enumerator, 2);
    }

    private static T GetNextElement<T>(IEnumerator<T> enumerator, int expectedSize)
    {
        if (!enumerator.MoveNext())
            throw new InvalidOperationException($"Collection expected to have {expectedSize} elements");
        return enumerator.Current;
    }
}