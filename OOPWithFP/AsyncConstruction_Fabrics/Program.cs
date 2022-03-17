using System;
using System.Threading.Tasks;

namespace AsyncConstruction_Fabrics
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyClass myClass= await MyClass.CreateAsync("asd");
            Console.WriteLine(myClass.Name);
        }
    }
}
