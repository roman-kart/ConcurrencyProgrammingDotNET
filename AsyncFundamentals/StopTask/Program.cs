namespace StopTask;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine($"Before task: {DateTime.Now:T}");
        await DelayResult(10, TimeSpan.FromSeconds(2));
        Console.WriteLine($"After task: {DateTime.Now:T}");


    }
    public static async Task<T> DelayResult<T>(T result, TimeSpan delay)
    {
        // Task.Delay(TimeSpan delay); - возвращает задачу, которая завершиться по истечению указанного времени
        await Task.Delay(delay);
        return result;
    }
}