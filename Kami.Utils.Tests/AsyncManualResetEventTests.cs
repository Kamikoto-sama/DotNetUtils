using Kami.Utils.SynchronizationTools;

namespace Kami.Utils.Tests;

[TestFixture]
public class AsyncManualResetEventTests
{
    [Test]
    public async Task BlocksThread_WhenWaitCalled()
    {
        var manualResetEvent = new AsyncManualResetEvent(false);

        var task = manualResetEvent.WaitAsync();
        await task.WithTimeout(10.Milliseconds());

        Assert.That(task.IsCompleted, Is.False);
    }

    [Test]
    [Timeout(10)]
    public async Task ReleasesThread_WhenSetCalled()
    {
        var manualResetEvent = new AsyncManualResetEvent(false);

        var task = manualResetEvent.WaitAsync();
        manualResetEvent.Set();
        await task.WithTimeout(1.Seconds());

        Assert.That(task.IsCompleted, Is.True);
    }

    [Test]
    [Timeout(10)]
    public void ThrowsDisposedException_WhenDisposedBeforeWaitCalled()
    {
        var manualResetEvent = new AsyncManualResetEvent(false);
        manualResetEvent.Dispose();

        Assert.ThrowsAsync<ObjectDisposedException>(() => manualResetEvent.WaitAsync());
    }
    
    [Test]
    [Timeout(10)]
    public void ThrowsDisposedException_WhenDisposedAfterWaitCalled()
    {
        var manualResetEvent = new AsyncManualResetEvent(false);
        var task = manualResetEvent.WaitAsync();
        manualResetEvent.Dispose();

        Assert.ThrowsAsync<ObjectDisposedException>(async () => await task);
    }
    
    [Test]
    public async Task DoesntThrows_WhenDisposedAfterSetCalled()
    {
        var manualResetEvent = new AsyncManualResetEvent(false);
        var task = manualResetEvent.WaitAsync();
        manualResetEvent.Set();
        manualResetEvent.Dispose();
        await task;
    }
}