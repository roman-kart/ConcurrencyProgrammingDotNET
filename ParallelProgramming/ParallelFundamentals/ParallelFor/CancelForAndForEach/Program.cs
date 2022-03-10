namespace CancelParallelLoops
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using RandomThings;

    class Program
    {
        static void Main()
        {
            int[] nums = Enumerable.Range(0, 10_000_000).ToArray();
            CancellationTokenSource cts = new CancellationTokenSource();
            // Use ParallelOptions instance to store the CancellationToken
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            Console.WriteLine("Press any key to start. Press 'c' to cancel.");
            Console.ReadKey();

            // Run a task so that we can cancel from another thread.
            var cancelTask = Task.Factory.StartNew(() =>
            {
                if (Console.ReadKey().KeyChar == 'c')
                    cts.Cancel();
                Console.WriteLine("press any key to exit");
            });

            try
            {
                Parallel.ForEach(nums, po, (num) =>
                {
                    double d = Math.Sqrt(num);
                    Console.WriteLine("{0} on {1}", d, Thread.CurrentThread.ManagedThreadId);
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                cancelTask.Dispose();
                cts.Dispose();
            }

            string[] words = new string[1_000_000];
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationToken.Token;
            parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            // завершить выполнение по нажатию клавиши C
            var cancelTaskStr = Task.Factory.StartNew(() =>
            {
                var inputChar = Console.ReadKey();
                while (inputChar.Key != ConsoleKey.C)
                {
                    inputChar = Console.ReadKey();
                }
                cancellationToken.Cancel();
            });

            RandomString randomString = new RandomString();
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = randomString.GetString();
            }

            try
            {
                Parallel.ForEach(words, parallelOptions, (word) =>
                {
                    Console.WriteLine($"{word.Replace('а', 'a').Replace('с', 'c')}. {Thread.CurrentThread.ManagedThreadId}");
                });
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cancelTaskStr.Dispose();
                cancellationToken.Dispose();
            }

            Console.ReadKey();
        }
    }
}