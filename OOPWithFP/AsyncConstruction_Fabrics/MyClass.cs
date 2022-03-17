using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncConstruction_Fabrics
{
    internal class MyClass
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; init; }
        private MyClass()
        {
            this.CreateDate = DateTime.Now;
        }
        private async Task<MyClass> InitialiseAsync(string name)
        {
            await Task.Delay(1);
            this.Name = name;
            return this;
        }
        public static async Task<MyClass> CreateAsync(string name)
        {
            MyClass result = new MyClass();
            return await result.InitialiseAsync(name);
        }
    }
}
