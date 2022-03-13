using System.Security.Cryptography;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var lists = new List<int>[10];
        for (int i = 0; i < lists.Length; i++)
        {
            lists[i] = GetIntList(1000);
        }

        int result = 0;
        Parallel.ForEach(source: lists,
            localInit: () => 0,
            body: (source, state, localValue) =>
            {
                foreach (var item in source)
                {
                    localValue += item;
                }
                return localValue;
            },
            localFinally: localValue =>
            {
                Interlocked.Add(ref result, localValue);
            });
        Console.WriteLine(result);
    }
    public static List<int> GetIntList(int count = 100)
    {
        var result = new List<int>();
        for (int i = 0; i < count; i++)
        {
            result.Add(RandomNumberGenerator.GetInt32(-100, 100));
        }
        return result;
    }
}