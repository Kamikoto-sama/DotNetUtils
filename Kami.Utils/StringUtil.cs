using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kami.Utils;

public static class StringExtensions
{
    public static int ToInt(this string value) => int.Parse(value);

    public static string ToBin(this long value) => Convert.ToString(value, 2).PadLeft(sizeof(long) * 8, '0');

    public static byte[] ToBytes(this string? value, Encoding? encoding = null) => (encoding ?? Encoding.UTF8).GetBytes(value ?? "");

    public static string FromBytes(this byte[] bytes, Encoding? encoding = null) => (encoding ?? Encoding.UTF8).GetString(bytes);

    public static string JoinToString<T>(this IEnumerable<T> source, string separator = ", ") => source.ToString(separator);

    public static string ToString<T>(this IEnumerable<T> source, string separator) => string.Join(separator, source);

    public static bool IsSignificant([NotNullWhen(true)] this string? value) => !string.IsNullOrWhiteSpace(value);

    public static string EnsureSignificant(this string? value, [CallerArgumentExpression("value")] string? name = null) =>
        value.IsSignificant() ? value : throw new ArgumentNullException(name);

    public static int GetStableHashCode(this string str)
    {
        unchecked
        {
            var hash1 = 5381;
            var hash2 = hash1;

            for (var i = 0; i < str.Length && str[i] != '\0'; i += 2)
            {
                hash1 = (hash1 << 5) + hash1 ^ str[i];
                if (i == str.Length - 1 || str[i + 1] == '\0')
                    break;
                hash2 = (hash2 << 5) + hash2 ^ str[i + 1];
            }

            return hash1 + hash2 * 1566083941;
        }
    }
}