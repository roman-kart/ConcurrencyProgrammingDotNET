using System;
using System.Threading.Tasks;


namespace AsyncProperties
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // асинхронных свойств в C# нет, так как это может вводить в заблуждение.
            // В лействительности, если возникла необходимость в асинхронном свойстве,
            // его нужно заменить асинхронным методом,
            Console.WriteLine(await MyClass.GetInt());

        }
    }
    internal class MyClass
    {
        /*
         * Неправильно!
        public int MyInt
        {
            async get 
            {
                var result = await Task.FromResult(10);
                return result;
            }
        }
        */
        public static async Task<int> GetInt()
        {
            await Task.Delay(2000);
            var result = await Task.FromResult(10);
            return result;
        }
        
    }
}
