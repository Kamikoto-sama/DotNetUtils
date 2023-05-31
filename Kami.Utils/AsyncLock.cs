using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kami.Utils;

public class AsyncLock : IDisposable
{
    public bool Locked => semaphore.CurrentCount == 0;

    private readonly SemaphoreSlim semaphore = new(1, 1);

    public Task<IDisposable> Obtain() => Obtain(TimeSpan.MaxValue, default);

    public Task<IDisposable> Obtain(CancellationToken ct) => Obtain(Timeout.InfiniteTimeSpan, ct);

    public Task<IDisposable> Obtain(TimeSpan timeout) => Obtain(timeout, default);

    public async Task<IDisposable> Obtain(TimeSpan timeout, CancellationToken ct)
    {
        await semaphore.WaitAsync(timeout, ct);
        return new LockHolder(semaphore);
    }

    private readonly struct LockHolder : IDisposable
    {
        private readonly SemaphoreSlim semaphore;

        public LockHolder(SemaphoreSlim semaphore)
        {
            this.semaphore = semaphore;
        }

        public void Dispose() => semaphore.Release();
    }

    public void Dispose() => semaphore.Dispose();
}