using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kami.Utils;

public class AsyncAutoResetEvent(bool signaled) : IDisposable
{
    private readonly CancellationTokenSource cts = new();
    private readonly Queue<TaskCompletionSource> waits = new();
    private bool signaled = signaled;

    public Task WaitAsync()
    {
        ObjectDisposedException.ThrowIf(cts.IsCancellationRequested, this);
        lock (waits)
        {
            if (signaled)
            {
                signaled = false;
                return Task.CompletedTask;
            }

            var tcs = new TaskCompletionSource();
            waits.Enqueue(tcs);
            return GetTask();

            async Task GetTask()
            {
                cts.Token.Register(() => tcs.TrySetResult());
                await tcs.Task;
                ObjectDisposedException.ThrowIf(cts.IsCancellationRequested, this);
            }
        }
    }

    public void Set()
    {
        TaskCompletionSource? toRelease;
        lock (waits)
            if (!waits.TryDequeue(out toRelease) && !signaled)
                signaled = true;
        toRelease?.TrySetResult();
    }

    public void Dispose() => cts.Cancel();
}