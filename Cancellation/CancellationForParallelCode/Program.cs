using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CancellationForParallelCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] digits = new int[10][];
            for (int i = 0; i < digits.Length; i++)
            {
                digits[i] = Enumerable.Range(i, i * RandomNumberGenerator.GetInt32(100_000, 1_000_000)).ToArray();
            }

            CancellationTokenSource token = new CancellationTokenSource(120);

            try
            {
                var parallelResult = Parallel.ForEach(digits, parallelOptions: new ParallelOptions() { CancellationToken = token.Token }, digitArr =>
                {
                    decimal sum = 0;
                    for (int i = 0; i < digitArr.Length; i++)
                    {
                        sum += digitArr[i];
                    }
                    Console.WriteLine($"Sum: {sum}");
                });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation cancelled!");
            }
            catch (Exception)
            {
                Console.WriteLine("Other exception!");
            }
        }
    }
}
