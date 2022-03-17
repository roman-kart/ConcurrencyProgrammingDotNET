using System;
using System.Threading.Tasks;

namespace AsyncInterfacesAndInheritance
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyAsyncClass myAsyncClass = new MyAsyncClass();
            await myAsyncClass.DoSomethingAsync(1);

            MyAsyncClassInhAbstract myAsyncClassInhAbstract = new MyAsyncClassInhAbstract();
            await myAsyncClassInhAbstract.DoSomethingAsync();
            await myAsyncClassInhAbstract.DoSomething1Async();
        }
    }
}
