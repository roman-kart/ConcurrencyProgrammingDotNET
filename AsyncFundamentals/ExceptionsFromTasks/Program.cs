namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        await TestAsync();

        var t = ThrowExceptionDelay();
        try
        {
            await t;
        }
        catch (InvalidOperationException ioe)
        {
            Console.WriteLine("Исключение передается вызывающему коду, когда он ожидает результат при помощи await.");
            Console.WriteLine(ioe.Message);
        }
    }
    public static async Task ThrowExceptionDelay()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        throw new InvalidOperationException("Test exception");
    }
    public static async Task TestAsync()
    {
        try
        {
            await ThrowExceptionDelay();
        }
        catch (InvalidOperationException ioe)
        {
            Console.WriteLine(ioe.Message);
        }
    }
}