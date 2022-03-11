namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var digits = new int[] { 5, 4, 3, 2, 1};
        var tasksSample = digits.Select(i => DelayAndReturnAsync(i));
        var tasks = tasksSample.ToArray();

        // wrong
        foreach (var task in tasks)
        {
            var result = await task;
            Console.WriteLine(result);
        }

        // ContinueWith
        var taskCV = tasksSample.Select(task => task.ContinueWith((i) =>
        {
            Console.WriteLine(i.Result);
        }));
        var tasksCVResult = taskCV.ToArray();
        await Task.WhenAll(tasksCVResult);

        // additional method for task

    }
    public static async Task<int> DelayAndReturnAsync(int value)
    {
        await Task.Delay(TimeSpan.FromSeconds(value));
        return value;
    }
}