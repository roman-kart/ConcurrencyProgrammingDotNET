using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CancelationAsync
{
    class WriteSiteInfoAsync
    {
        public CancellationTokenSource CancellationToken; // объект, который содержит информацию о том, было ли отменено действие
        private HttpClient HttpClient = new HttpClient // объект для работы с http-запросами
        {
            MaxResponseContentBufferSize = 1_000_000
        };
        // метод, который завершает работу асинхронного метода, если пользователь нажимает ENTER
        public void CancelAction()
        {
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("Press the ENTER key to cancel...");
            }

            Console.WriteLine("\n ENTER key pressed: cancelling download.\n");
            StopExecuting?.Invoke();
        }
        // событие, которое хранит методы, выполняемые при завершении приложения
        public event Action StopExecuting;
        // основной метод вывода информации на экран
        public async Task Show(IEnumerable<string> urls)
        {
            CancellationToken = new CancellationTokenSource();
            StopExecuting += () =>
            {
                this.CancellationToken.Cancel();
            };

            Console.WriteLine("Application started.");
            Console.WriteLine("Press the ENTER key to cancel...\n");

            Task cancelTask = Task.Run(CancelAction);
            Task sumPageSize = SumPageSizeAsync(urls);
            await Task.WhenAny(new[] { cancelTask, sumPageSize });
			Console.WriteLine("Application ending!");
        }
        private async Task SumPageSizeAsync(IEnumerable<string> urls)
        {
            var stopwatch = Stopwatch.StartNew();

            int total = 0;
            foreach (var url in urls)
            {
                int contentLength = await ProcessUrlAsync(url, this.HttpClient, this.CancellationToken.Token);
            }

            stopwatch.Stop();

            Console.WriteLine($"\nTotal bytes returned:  {total:#,#}");
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        }
        private async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken cancellationToken)
		{
            var response = await client.GetAsync(url, cancellationToken);
            byte[] content = await response.Content.ReadAsByteArrayAsync(cancellationToken);
			Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

            return content.Length;
		}
    }
}
