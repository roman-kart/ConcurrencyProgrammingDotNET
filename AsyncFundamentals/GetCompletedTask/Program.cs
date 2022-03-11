namespace GetCompletedTask;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        PlugCancelled plugCancelled = new PlugCancelled();
        PlugCompletedTask plugCompletedTask = new PlugCompletedTask();
        PlugException plugException = new PlugException();
        PlugResult plugResult = new PlugResult();

        // Plug result
        Console.WriteLine(await plugResult.GetInt());

        // Plug exception
        try
        {
            Console.WriteLine(await plugException.GetInt());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        // Plug cancellation token
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        Console.WriteLine(await plugCancelled.GetInt(cancellationTokenSource.Token));
        cancellationTokenSource.Cancel();
        try
        {
            Console.WriteLine(await plugCancelled.GetInt(cancellationTokenSource.Token));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        // Plug completed task
        await plugCompletedTask.GetTask();
    }
}