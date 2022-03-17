using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInterfacesAndInheritance
{
    internal class MyAsyncClassInhAbstract : MyAbstractClass
    {
        new public async Task DoSomething1Async()
        {
            Console.WriteLine("New method!");
        }
        public override async Task DoSomethingAsync()
        {
            Console.WriteLine("Mining bitcoin...");
            await Task.Delay(1000);
            Console.WriteLine("Done!");
        }
    }
}
