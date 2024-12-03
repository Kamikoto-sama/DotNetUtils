using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kami.Utils;

public static class TaskUtils
{
    public static Task WhenAll(this IEnumerable<Task> tasks) => Task.WhenAll(tasks);

    public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks) => Task.WhenAll(tasks);

    public static Task<Task<T>> WhenAny<T>(this IEnumerable<Task<T>> source) => Task.WhenAny(source);

    public static void Await(this Task task) => task.GetAwaiter().GetResult();

    public static T Await<T>(this Task<T> task) => task.GetAwaiter().GetResult();

    public static Task WithCancellation(this Task task, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        cancellationToken.Register(obj => ((TaskCompletionSource)obj!).SetResult(), tcs);
        return Task.WhenAny(task, tcs.Task);
    }

    public static Task WithTimeout(this Task task, TimeSpan timeout) => Task.WhenAny(task, Task.Delay(timeout));

    public static async Task HandleCancellation(this Task task)
    {
        try
        {
            await task;
        }
        catch (TaskCanceledException)
        {
        }
    }

    public static async Task<T> HandleCancellation<T>(this Task<T> task, T defaultValue)
    {
        try
        {
            return await task;
        }
        catch (TaskCanceledException)
        {
            return defaultValue;
        }
    }
}