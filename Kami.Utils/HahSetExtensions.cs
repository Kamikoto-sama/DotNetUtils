using System.Collections.Generic;

namespace Kami.Utils;

public static class HahSetExtensions
{
    public static void RemoveRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
    {
        foreach (var item in items)
            hashSet.Remove(item);
    }

    public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
    {
        foreach (var item in items)
            hashSet.Add(item);
    }
}