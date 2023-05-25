using System;

namespace Kami.Utils;

public static class GenericExtensions
{
    /// <summary>
    /// Allows using any function as extension method to chain calls
    /// </summary>
    public static TResult Chain<TSource, TResult>(this TSource source, Func<TSource, TResult> extension) => extension(source);
}