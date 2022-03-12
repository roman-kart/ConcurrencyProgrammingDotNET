using System.Numerics;
using System.Security.Cryptography;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var count = new List<int>();
        for (int i = 0; i < 100; i++)
        {
            count.Add(RandomNumberGenerator.GetInt32(100, 200));
        }
        Parallel.ForEach(count, i =>
        {
            Console.WriteLine(RandomString(i));
        });

        Parallel.ForEach(count, (digit, state) =>
        {
            if (digit % 2 == 0)
            {
                state.Stop();
            }
            else
            {
                Console.WriteLine(RandomString(digit));
            }
        });

        int totalLength = 0;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ParallelOptions parallelOptions = new ParallelOptions();
        parallelOptions.CancellationToken = cancellationTokenSource.Token;
        Parallel.ForEach(count, parallelOptions, digit =>
        {
            var result = RandomString(digit);
            Console.WriteLine(result);
            Interlocked.Add(ref totalLength, digit);
        });
        Console.WriteLine($"Total length = {totalLength}");
    }
    public static string RandomString(int length)
    {
        BigInteger bigInteger = 0;
        for (int i = 0; i < 1000; i++)
        {
            bigInteger += RandomNumberGenerator.GetInt32(-10000, 10000);
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sb.Append((char)RandomNumberGenerator.GetInt32(1040, 1073));
        }
        return sb.ToString();
    }
}