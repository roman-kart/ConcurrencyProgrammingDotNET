using System;
using System.Linq;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // такой вариант можно использовать для небольшого кол-ва задач
            Console.WriteLine("Hello World!");
            var tasks =
                from i in new int[] { 100, 200, 300, 400, 500 }
                select WriteTenRowDelay($"Delay{i}", i);
            var writeTasks = tasks.ToList();

            int finishedTaskCount = 0;
            while (writeTasks.Any())
            {
                var finishedTask = await Task.WhenAny(writeTasks);
                writeTasks.Remove(finishedTask);
                finishedTaskCount++;
                Console.WriteLine($"Task finished! Finished tasks: {finishedTaskCount}");
            }

            // для большого кол-ва задач
            var largeArr = new int[64_000];
            for (int i = 0; i < largeArr.Length; i++)
            {
                largeArr[i] = RandomNumberGenerator.GetInt32(100, 500);
            }
            var largeTasks =
                from i in largeArr
                select WriteTenRowDelay($"Task{i}", i, false).ContinueWith((t) =>
                {
                    Console.Write("Task finished!");
                });
            var largeTaskList = largeTasks.ToList();
            Stopwatch stopwatch = Stopwatch.StartNew();
            await Task.WhenAll(largeTaskList);
            stopwatch.Stop();
            var continueWith = stopwatch.ElapsedMilliseconds;

            largeTasks =
                from i in largeArr
                select WriteTenRowDelay($"Task{i}", i, false);
            var largeTasksList = largeTasks.ToList();

            stopwatch = Stopwatch.StartNew();
            while (largeTaskList.Any())
            {
                var finishedTask = await Task.WhenAny(largeTaskList);
                largeTaskList.Remove(finishedTask);
                Console.Write("Task finished");
            }
            stopwatch.Stop();
            var enumerationTask = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Continue with: {continueWith}"); // 5163
            Console.WriteLine($"Enumeration task: {enumerationTask}"); // 92410

            Console.ReadKey();
        }
        static async Task WriteTenRowDelay(string name, int delay, bool isVisible = true)
        {
            for (int i = 0; i < 10; i++)
            {
                if (isVisible)
                {
                    Console.WriteLine($"{name}: Row№{i}");
                }
                await Task.Delay(delay);
            }
        }
    }
}
