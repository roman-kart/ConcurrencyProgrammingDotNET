using Nito.AsyncEx;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static void Main(string[] args)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    // плохой код, лучше использовать Task вместо void
    public static async void MainAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        throw new Exception("Your dad isn't found!");
    }
}