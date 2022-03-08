using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncFundamentals
{
    internal class TestAsyncFirst
    {
        // асинхронный успех
        public async Task<T> DelayResult<T>(T result, TimeSpan delay)
        {
            await Task.Delay(delay);
            return result;
        }
        public async Task<T> DelayException<T>(T result, TimeSpan delay)
        {
            await Task.Delay(delay);
            throw new Exception("Error with doing something!");
        }
    }
}
