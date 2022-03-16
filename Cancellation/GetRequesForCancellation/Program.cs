using System;
using System.Threading;
using System.Threading.Tasks;

namespace GetRequesForCancellation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource token = new CancellationTokenSource();

            var write10 = WriteAsync(10, token.Token);
            var write1000 = WriteAsync(1000, token.Token);
            var write200 = WriteAsync(200, token.Token);

            await Task.Delay(1_000);
            token.Cancel();

            await Task.WhenAll(write10, write200, write1000);
        }
        static async Task WriteAsync(decimal start = 0, CancellationToken cancellationToken = default)
        {
            decimal digit = start;
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1);
                Console.WriteLine(digit++);
            }
        }
    }
}
