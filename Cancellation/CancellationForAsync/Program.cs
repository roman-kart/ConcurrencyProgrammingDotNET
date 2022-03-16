using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationForAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource token = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            var write10 = WriteAsync(10, token.Token);
            var write200 = WriteAsync(200, token.Token);
            var write1000 = WriteAsync(1000, token.Token);

            try
            {
                await Task.WhenAll(write10, write200, write1000);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation cancelled");
            }
            catch (Exception)
            {
                Console.WriteLine("Non OperationCanceledException");
            }
        }
        static async Task WriteAsync(decimal start = 0, CancellationToken cancToken = default, bool genEx = false)
        {
            if (genEx)
            {
                throw new Exception("Non OperationCanceledException");
            }

            decimal digit = start;
            for (decimal i = digit; ; i++)
            {
                await Task.Delay(1, cancToken);
                Console.WriteLine(i);
            }
        }
    }
}
