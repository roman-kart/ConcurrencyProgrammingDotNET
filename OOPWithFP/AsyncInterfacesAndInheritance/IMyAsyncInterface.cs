using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInterfacesAndInheritance
{
    internal interface IMyAsyncInterface
    {
        // public async Task DoSomethingAsync(); // не будет работать! Применять async можно только к методам, имеющим реализацию
        public Task DoSomethingAsync(int seconds); // будет работать, в типе, реализующем данный метод, можно добавить модификатор async
    }
}
