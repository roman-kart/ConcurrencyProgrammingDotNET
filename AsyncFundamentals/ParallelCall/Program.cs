using System.Numerics;
using System.Diagnostics;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        Parallel.Invoke(CalcReallyHeavy, GetReallyManyData);
    }
    public static void CalcReallyHeavy()
    {
        BigInteger bigInteger = 0;
        
        for (int j = 0; j < int.MaxValue; j++)
        {
            bigInteger += j;
        }
        Console.WriteLine(bigInteger.ToString());
    }
    public static void GetReallyManyData()
    {
        HttpClient httpClient = new HttpClient();
        var urls = new string[]
        {
            "https://habr.com/ru/post/482354/",
            "https://www.youtube.com/watch?v=C4MpzSMkinw"
        };
        for (int i = 0; i < urls.Length; i++)
        {
            BigInteger totalTime = 0;
            for (int j = 0; j < 10; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var task = httpClient.GetAsync(urls[i]);
                var result = task.Result;
                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
            }
            Console.WriteLine($"{urls[i]}: {totalTime}");
        }
    }
}