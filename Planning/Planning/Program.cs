using System;
using System.Threading;
using System.Threading.Tasks;

namespace Planning
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Task.Run - вынесение задачи из текущего потока в пул потоков.
            // идеально для UI-приложений с долговыполняющимися операциями.
            // НЕ ИСПОЛЬЗОВАТЬ В ASP.NET

            var task = Task.Run(() =>
            {
                Console.WriteLine("Wait 2 seconds, please!");
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine("Thanks!");
            });

            Console.WriteLine("Before task's completing!");

            
            // выполнение кода при помощи планировщика задач

            // TaskScheduler.Default // ставит работу в очередь пула потоков, используется по умолчанию, например, в
            // Task.Run параллельном коде и коде потоков данных
            
            await Task.WhenAll(task);
        }
    }
}
