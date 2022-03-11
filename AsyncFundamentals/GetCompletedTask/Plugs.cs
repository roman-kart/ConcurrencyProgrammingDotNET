using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCompletedTask
{
    internal class PlugResult : IAsyncInterface
    {
        public Task<int> GetInt()
        {
            return Task<int>.FromResult(10); // Task.FromResult(x) - возвращает успешно выполненную задачу с результатом
        }
    }
    public class PlugException : IAsyncInterface
    {
        public Task<int> GetInt()
        {
            return Task.FromException<int>(new Exception("Uff, something is wrong!")); // возвращает задачу, которая не выполнилась из-за ошибки
        }
    }
    public class PlugCancelled
    {
        public Task<int> GetInt(CancellationToken cancellationTokenSource)
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                return Task.FromCanceled<int>(cancellationTokenSource); // возвращает задачу, которая не вополнилась из-за токена завершения
            }
            return Task.FromResult<int>(10);
        }
    }
    public class PlugCompletedTask
    {
        public Task GetTask()
        {
            Console.WriteLine("Completed task");
            return Task.CompletedTask; // завершенная задача без возвращаемого значения (хранится в кэшированном объекте)
        }
    }
}
