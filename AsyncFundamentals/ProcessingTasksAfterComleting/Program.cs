using System.Diagnostics;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var digits = new int[] { 5, 4, 3, 2, 1};
        var tasksSample = digits.Select(i => DelayAndReturnAsync(i));
        var tasks = tasksSample.ToArray();

        // wrong
        Console.WriteLine("Wrong: ");
        foreach (var task in tasks)
        {
            var result = await task;
            Console.WriteLine(result);
        }

        // ContinueWith
        Console.WriteLine("Continue with: ");
        var taskCV = tasksSample.Select(task => task.ContinueWith((i) =>
        {
            Console.WriteLine(i.Result);
        }));
        var tasksCVResult = taskCV.ToArray();
        await Task.WhenAll(tasksCVResult);

        // additional method for task
        Console.WriteLine("Additional method: ");
        var taskQuery =
            from t in tasksSample
            select AwaitAndProcessAsync(t);
        var processingTask = taskQuery.ToArray();
        await Task.WhenAll(processingTask);

        // without explicit additional method
        Console.WriteLine("Without explicit additional method");
        processingTask = tasksSample.Select(async t =>
        {
            var result = await t;
            Console.WriteLine(result);
        }).ToArray();
        await Task.WhenAll(processingTask);
    }
    public static async Task<int> DelayAndReturnAsync(int value)
    {
        await Task.Delay(TimeSpan.FromSeconds(value));
        return value;
    }
    public static async Task AwaitAndProcessAsync(Task<int> task)
    {
        int result = await task;
        Console.WriteLine(result);
    }
}