using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* синхронизация должна использоваться при выполнении всех следующих условий:
             * 1) несколько частей кода выполняются одновременно 
             * 2) эти части кода обращаются к одним и тем де данным
             * 3) одна часть кода обновляет или записывает данные
             */

        }
        // блокировка при помощи lock
        /// <summary>
        /// Правила при использовании блокировок:
        /// 1) ограничить видимость блокировки,
        /// 2) документировать, что именно защищает блокировка,
        /// 3) сократить до минимума объем кода, защищенной блокировкой
        /// 4) Никогда не выполнять произвольный код при использовании блокировки
        /// </summary>
        public class BlockingByLock
        {
            private readonly object _mutex = new object();
            private int value;
            public void IncreaseValue(int digit)
            {
                // в данный участок кода может войти только один поток
                lock (_mutex)
                {
                    value += digit;
                }
            }
        }
        public class BlockingByAsync
        {
            private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);

            private int _value;

            public async Task DelayAndIncreaseValueAsync(int digit)
            {
                await _mutex.WaitAsync(); // ожидаем, когда поле освободится
                // пытаемся увеличить значение
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    _value += digit;
                }
                finally
                {
                    _mutex.Release(); // убираем блокировку
                }
            }
        }
        // отправить уведомление от одного потока другому
        public class BlockingSignals
        {
            private readonly ManualResetEventSlim _initialized = new ManualResetEventSlim();

            private int _value;

            public int WaitForInitialization()
            {
                _initialized.Wait();
                return _value;
            }
            public void InitializedSet()
            {
                _value = 13;
                _initialized.Set();
            }
        }
        // если уведомление должны быть отправлено только один раз
        public class AsyncSignals
        {
            private readonly TaskCompletionSource<object> _initialized = new TaskCompletionSource<object>();

            private int _value1;
            private int _value2;

            private async Task<int> WaitForInitialization()
            {
                await _initialized.Task;
                return _value1 + _value2;
            }
            public void Intiialize()
            {
                _value2 = 13;
                _value1 = 12;
                _initialized.TrySetResult(null);
            }
        }
    }
}
