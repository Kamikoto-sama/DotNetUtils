using System;
using System.Threading;

namespace Kami.Utils;

public class TimeoutTimer : IDisposable
{
    private bool started;
    private readonly TimeSpan timeoutDelay;
    private readonly bool repeat;
    private readonly Timer innerTimer;

    public event Action? OnTick;

    public TimeoutTimer(TimeSpan timeoutDelay, bool repeat)
    {
        this.timeoutDelay = timeoutDelay;
        this.repeat = repeat;
        innerTimer = new Timer(InvokeTick, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
    }

    public void Start()
    {
        lock (this)
        {
            if (started)
                return;
            Restart();
            started = true;
        }
    }

    public void Restart()
    {
        var period = repeat ? timeoutDelay : Timeout.InfiniteTimeSpan;
        innerTimer.Change(timeoutDelay, period);
    }

    private void InvokeTick(object? state) => OnTick?.Invoke();

    public void Dispose() => innerTimer.Dispose();
}