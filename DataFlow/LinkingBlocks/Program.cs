using System.Threading.Tasks.Dataflow;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var ParseProduct = new TransformBlock<string, Product>(str =>
        {
            var data = str.Split('|', StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 2)
            {
                throw new InvalidDataException("Invalid data of product!\n" +
                    "String must be like: Product name | Product price");
            }
            else
            {
                return new Product() { Name = data[0], Price = Decimal.Parse(data[1]) };
            }
        });

        var ShowProduct = new ActionBlock<Product>(prod =>
        {
            Console.WriteLine(prod.ToString());
        });

        var options = new DataflowLinkOptions() { PropagateCompletion = true }; // для распростанения завершений и ошибок
        ParseProduct.LinkTo(ShowProduct, options); // после завершения работы первого блока, результат работы первого блока передается в следующмй блок
        // ошибки распространяются в виде AggregateException

        string[] prodsStr = new string[]
        {
            "Banana | 100",
            "Pickles | 200",
            "Soap | 19",
            "Cake | 200",
            "Cookies | 123"
        };

        foreach (var prodStr in prodsStr)
        {
            ParseProduct.Post(prodStr);
        }
        ParseProduct.Complete();
        await ShowProduct.Completion;
    }
}
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public override string ToString()
    {
        return $"{Name}: {Price}";
    }
}