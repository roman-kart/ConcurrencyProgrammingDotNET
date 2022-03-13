using System.Threading.Tasks.Dataflow;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    // ЗАПУСКАТЬ ИЗ КОНСОЛИ
    public static async Task Main(string[] args)
    {
        try
        {
            var block = new TransformBlock<int, int>(DoSom);
            
            block.Post(1);

            await block.Completion;
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
    public static int DoSom(int i)
    {
        if (i == 1)
        {
            throw new ArgumentException("Can't be 1");
        }
        return i * 2;
    }
}