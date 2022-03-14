using System.Security.Cryptography;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        
    }
    public static async Task GetStrArr(int arrLength)
    {
        string[] strs = new string[arrLength];
        // использовать при работе с UI! В ASP.NET уже есть встроенная параллельная обработка (по идее)
        var task = Task.Run(() =>
        {
            Parallel.For(0, arrLength, i =>
            {
                var wordLength = RandomNumberGenerator.GetInt32(1, 100);
                char[] chars = new char[wordLength];
                for (int j = 0; j < chars.Length; j++)
                {
                    chars[j] = (char)RandomNumberGenerator.GetInt32(1040, 1073);
                }
                strs[i] = new string(chars);
            });
        });
        await task;
    }
}