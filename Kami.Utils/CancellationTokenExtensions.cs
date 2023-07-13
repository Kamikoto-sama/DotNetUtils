using System.Threading;
using System.Threading.Tasks;

namespace Kami.Utils;

public static class CancellationTokenExtensions
{
    public static Task WaitForCancellation(this CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        cancellationToken.Register(s => ((TaskCompletionSource)s!).SetResult(), tcs);
        return tcs.Task;
    }
}