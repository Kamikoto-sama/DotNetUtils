using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kami.Utils.SynchronizationTools;

public class AsyncManualResetEvent(bool signaled) : IDisposable
{
    private readonly CancellationTokenSource cts = new();
    private TaskCompletionSource? tcs;
    private bool signaled = signaled;

    public Task WaitAsync(CancellationToken token = default)
    {
        ObjectDisposedException.ThrowIf(cts.IsCancellationRequested, this);
        lock (cts)
        {
            if (signaled)
                return Task.CompletedTask;
            if (tcs != null)
                return GetTask();

            tcs = new TaskCompletionSource();
            cts.Token.Register(() => tcs.TrySetCanceled());
            return GetTask();

            async Task GetTask()
            {
                await tcs.Task.WithCancellation(token);
                ObjectDisposedException.ThrowIf(cts.IsCancellationRequested, this);
            }
        }
    }

    public void Set()
    {
        lock (cts)
        {
            if (signaled)
                return;
            
            signaled = true;
            tcs?.TrySetResult();
        }
    }

    public void Reset()
    {
        lock (cts)
        {
            if (!signaled)
                return;
            
            signaled = false;
        }
    }

    public void Dispose()
    {
        cts.Cancel();
        cts.Dispose();
    }
}