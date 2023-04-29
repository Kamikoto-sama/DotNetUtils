using System;

namespace Kami.Utils;

public static class TimeSpanExtensions
{
    public static TimeSpan Milliseconds(this int seconds) => TimeSpan.FromMilliseconds(seconds);
    public static TimeSpan Milliseconds(this double seconds) => TimeSpan.FromMilliseconds(seconds);
    public static TimeSpan Seconds(this int seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan Seconds(this double seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan Minutes(this int seconds) => TimeSpan.FromMinutes(seconds);
    public static TimeSpan Minutes(this double seconds) => TimeSpan.FromMinutes(seconds);
    public static TimeSpan Hours(this int seconds) => TimeSpan.FromHours(seconds);
    public static TimeSpan Hours(this double seconds) => TimeSpan.FromHours(seconds);
    public static TimeSpan Days(this int seconds) => TimeSpan.FromDays(seconds);
    public static TimeSpan Days(this double seconds) => TimeSpan.FromDays(seconds);
    public static TimeSpan Ticks(this int seconds) => TimeSpan.FromTicks(seconds);
    public static TimeSpan Ticks(this long seconds) => TimeSpan.FromTicks(seconds);
}