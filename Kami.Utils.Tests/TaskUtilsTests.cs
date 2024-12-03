namespace Kami.Utils.Tests;

[TestFixture]
public class TaskUtilsTests
{
    [Test]
    [Timeout(100)]
    public async Task NoException_WhenHandleCancellation()
    {
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await Task.Delay(5.Seconds(), cts.Token).HandleCancellation();
    }

    [Test]
    [Timeout(100)]
    public async Task NoException_WhenGenericHandleCancellation()
    {
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        async Task<int> Do(CancellationToken ct)
        {
            await Task.Delay(5.Seconds(), ct);
            return 10;
        }

        var result = await Do(cts.Token).HandleCancellation(1);
        Assert.That(result, Is.EqualTo(1));
    }
}