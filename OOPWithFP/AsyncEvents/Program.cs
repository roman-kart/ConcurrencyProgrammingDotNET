using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncEvents
{
    class MyClassInventory
    {
        public CancellationTokenSource tokenSource { get; set; }
        public MyClass MyClass { get; set; }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyClassInventory[] myClasses = new MyClassInventory[]
            {
                new MyClassInventory{MyClass = new MyClass(), tokenSource = new CancellationTokenSource()},
                new MyClassInventory{MyClass = new MyClass(), tokenSource = new CancellationTokenSource()},
                new MyClassInventory{MyClass = new MyClass(), tokenSource = new CancellationTokenSource()},
                new MyClassInventory{MyClass = new MyClass(), tokenSource = new CancellationTokenSource()},
            };
            int i = 3;
            foreach (var classInventory in myClasses)
            {
                var task = classInventory.MyClass.DoSomething(classInventory.tokenSource);
                await Task.Delay(TimeSpan.FromSeconds(i++));
                classInventory.MyClass.Dispose();

                try
                {
                    await task;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Cancelled!");
                }
            }

            await using (MyClassAsyncDispose cl = new MyClassAsyncDispose())
            {
                Console.WriteLine(cl.ToString());
            }
        }
    }
    internal class MyClass : IDisposable
    {
        private CancellationTokenSource classToken = new CancellationTokenSource();
        public async Task DoSomething(CancellationTokenSource token)
        {
            var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(token.Token, this.classToken.Token);
            await Task.Delay(TimeSpan.FromSeconds(4), combinedToken.Token);
            Console.WriteLine("Result");
        }
        public void Dispose()
        {
            Console.WriteLine("Dispose");
            classToken.Cancel();
        }
    }
    internal class MyClassAsyncDispose : IAsyncDisposable
    {
        public MemoryStream memoryStream { get; set; } = new MemoryStream();
        public async ValueTask DisposeAsync()
        {
            memoryStream.Dispose();
            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine("Class has been disposed asynchronisely");
        }
    }
}
