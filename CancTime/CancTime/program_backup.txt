using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Canctime{
    sealed class Program
    {
        // framework не позволяет сделать асинхронный Main
        public static void Main(string[] args){
            MainAsync(args); // вызывает асинхронный метод Main
        }
        // метод, где содержится основная логика программы
        public static async Task MainAsync(string[] args){
            CancellationTokenSource cancToken = new CancellationTokenSource();
            cancToken.CancelAfter(3500);

            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                var taskA = AsyncMethods.ShowHundredRowsDelay("Say hello method", 100, cancToken.Token);
                var taskB = AsyncMethods.ShowHundredRowsDelay("Say goodbye", 300, cancToken.Token);
                var taskC = AsyncMethods.ShowHundredRowsDelay("Say something method", 1000, cancToken.Token);
                var t = Task.WhenAll(taskA, taskB, taskC);
                t.Wait();
                stopwatch.Stop();
                System.Console.WriteLine("Executing time: {0}", stopwatch.ElapsedMilliseconds);
            }
            catch (OperationCanceledException ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}