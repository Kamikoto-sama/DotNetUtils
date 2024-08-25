namespace Kami.Utils.Tests;

[TestFixture]
public class AsyncAutoResetEventTests
{
    [Test]
    public async Task BlocksThread_WhenWaitCalled()
    {
        var autoResetEvent = new AsyncAutoResetEvent(false);

        var task = autoResetEvent.WaitAsync();
        await task.WithTimeout(10.Milliseconds());

        Assert.That(task.IsCompleted, Is.False);
    }

    [Test]
    [Timeout(10)]
    public async Task ReleasesThread_WhenSetCalled()
    {
        var autoResetEvent = new AsyncAutoResetEvent(false);

        var task = autoResetEvent.WaitAsync();
        autoResetEvent.Set();
        await task.WithTimeout(1.Seconds());

        Assert.That(task.IsCompleted, Is.True);
    }

    [Test]
    public void ThrowsDisposedException_WhenDisposedBeforeWaitCalled()
    {
        var autoResetEvent = new AsyncAutoResetEvent(false);
        autoResetEvent.Dispose();

        Assert.ThrowsAsync<ObjectDisposedException>(autoResetEvent.WaitAsync);
    }
    
    [Test]
    public void ThrowsDisposedException_WhenDisposedAfterWaitCalled()
    {
        var autoResetEvent = new AsyncAutoResetEvent(false);
        var task = autoResetEvent.WaitAsync();
        autoResetEvent.Dispose();

        Assert.ThrowsAsync<ObjectDisposedException>(async () => await task);
    }
    
    [Test]
    public async Task DoesntThrows_WhenDisposedAfterSetCalled()
    {
        var autoResetEvent = new AsyncAutoResetEvent(false);
        var task = autoResetEvent.WaitAsync();
        autoResetEvent.Set();
        autoResetEvent.Dispose();
        await task;
    }
}