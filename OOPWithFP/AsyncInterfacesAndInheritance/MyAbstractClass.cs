using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInterfacesAndInheritance
{
    internal abstract class MyAbstractClass
    {
        // public abstract async Task DoSomethingAsync(); // к методу, помеченному как abstract, нельзя применять модификатор async
        public abstract Task DoSomethingAsync();
        public async Task DoSomething1Async()
        {
            await Task.Delay(1000);
        }
    }
}
