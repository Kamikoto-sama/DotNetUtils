using System.Diagnostics;

namespace Kami.Utils.Tests;

[TestFixture]
public class CancellationTokenExtensionsTests
{
    [Timeout(1000)]
    [Test]
    public async Task WaitForCancellation_Returns_WhenTokenCanceled()
    {
        var cts = new CancellationTokenSource();
        var ct = cts.Token;

        var task = ct.WaitForCancellation();
        var sw = Stopwatch.StartNew();
        _ = Task.Run(() =>
        {
            Thread.Sleep(100);
            cts.Cancel();
        });
        await task;
        sw.Stop();

        Assert.That(sw.Elapsed, Is.GreaterThan(100.Milliseconds()));
    }
}