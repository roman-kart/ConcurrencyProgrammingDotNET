using System.Runtime.CompilerServices;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        await foreach (var digit in SlowRange())
        {
            Console.WriteLine(digit);
        }

        using var cts = new CancellationTokenSource(500);
        CancellationToken token = cts.Token;
        try
        {
            await foreach (var digit in SlowRange(token))
            {
                Console.WriteLine(digit);
            }
        }
        catch (TaskCanceledException tce)
        {
            Console.WriteLine(tce.Message);
        }

        try
        {
            await ConsumeSequence(SlowRange());
        }
        catch (TaskCanceledException tce)
        {
            Console.WriteLine(tce.Message);
        }
    }
    public static async IAsyncEnumerable<int> SlowRange([EnumeratorCancellation] CancellationToken token = default)
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(i * 100, token);
            yield return i;
        }
    }
    public static async Task ConsumeSequence(IAsyncEnumerable<int> digits)
    {
        var cts = new CancellationTokenSource(500);
        CancellationToken token = cts.Token;
        await foreach (var digit in digits.WithCancellation(token))
        {
            Console.WriteLine(digit);
        }
    }
}