using System;
using System.Threading.Tasks;

namespace AsyncConstructionAsyncInitialise
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyClass myClass = new MyClass("ААА-БББ-ГГГ");
            Console.WriteLine(myClass.Name);
            await myClass.Initialisation;
            Console.WriteLine(myClass.Name);
        }
    }
    internal class MyClass
    {
        public string Name { get; set; }
        public Task Initialisation { get; private set; }
        public MyClass(string name)
        {
            this.Initialisation = Initialise(name);
        }
        private async Task Initialise(string name)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            this.Name = name;
        }
    }
}
