namespace Kami.Utils.Tests;

[TestFixture]
public class TaskUtilsTests
{
    [Test]
    [Timeout(100)]
    public async Task NoException_WhenHandleCancellation()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();

        await Task.Delay(5.Seconds(), cts.Token).HandleCancellation();
    }
}