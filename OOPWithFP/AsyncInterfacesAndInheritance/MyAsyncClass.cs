using System;
using System.Text.Encodings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInterfacesAndInheritance
{
    internal class MyAsyncClass : IMyAsyncInterface
    {
        public async Task DoSomethingAsync(int seconds)
        {
            Console.WriteLine($"Please, wait {seconds} seconds...");
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            Console.InputEncoding = Encoding.UTF8;
            Console.Write('☣');
            Console.Write('ツ');
            Console.WriteLine('☭');
        }
    }
}
