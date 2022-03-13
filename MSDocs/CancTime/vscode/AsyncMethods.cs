using System;
using System.Threading;
using System.Threading.Tasks;

namespace Canctime{
    sealed class AsyncMethods
    {
        public static async Task ShowHundredRowsDelay(string name, int delay, CancellationToken token){
            for (int i = 0; i < 100; i++)
            {
                System.Console.WriteLine("{0}: row№{1}", name, i);
                await Task.Delay(delay);
                // завершаем работу метода, если переданный токен содержит информацию о том, что необходимо завершить выполнение операций
                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}