using System;

namespace Kami.Utils;

public static class TimeSpanMath
{
    public static TimeSpan Max(TimeSpan value1, TimeSpan value2) => value1 > value2 ? value1 : value2;

    public static TimeSpan Min(TimeSpan value1, TimeSpan value2) => value1 < value2 ? value1 : value2;
}