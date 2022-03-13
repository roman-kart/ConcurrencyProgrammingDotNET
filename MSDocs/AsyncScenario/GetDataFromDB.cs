using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncScenario
{
    internal class GetDataFromDB
    {
        public class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
        public static async ValueTask<List<Product>> GetProducts()
        {
            var products = new List<Product>();
            products.Add(new Product() { Name = "Chocolate", Price = 100 });
            products.Add(new Product() { Name = "Milk", Price = 70 });
            await Task.Delay(8000);
            return new List<Product>(products);
        }
    }
}
