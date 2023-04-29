using System.Collections.Generic;
using System.Linq;

namespace Kami.Utils;

public static class AsyncEnumerableExtensions
{
    public static async IAsyncEnumerable<IAsyncEnumerable<T>> BatchAsync<T>(this IAsyncEnumerable<T> source, int batchSize)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        while (await enumerator.MoveNextAsync())
            yield return Enumerate(enumerator).Take(batchSize);

        async IAsyncEnumerable<T> Enumerate(IAsyncEnumerator<T> x)
        {
            yield return x.Current;
            while (await x.MoveNextAsync())
                yield return x.Current;
        }
    }
}