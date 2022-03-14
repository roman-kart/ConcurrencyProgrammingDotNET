using System.Collections.Immutable;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        // коллекция ключ-значение, которая не слишком часто изменяется,
        // и к которой смогут обращаться несколько потоков

        ImmutableDictionary<int, string> dict = ImmutableDictionary<int, string>.Empty;
        dict = dict.SetItem(10, "ten");
        dict = dict.SetItem(11, "eleven");
        dict = dict.SetItem(12, "twelve");
        dict = dict.SetItem(8, "eight");
        dict = dict.SetItem(7, "seven");
        Console.WriteLine($"{nameof(dict)} trace: ");
        foreach (var item in dict)
        {
            Console.WriteLine($"  {item}");
        }

        ImmutableSortedDictionary<int, string> sortedDict = ImmutableSortedDictionary<int, string>.Empty;
        sortedDict = sortedDict.SetItem(10, "ten");
        sortedDict = sortedDict.SetItem(11, "eleven");
        sortedDict = sortedDict.SetItem(12, "twelve");
        sortedDict = sortedDict.SetItem(8, "eight");
        sortedDict = sortedDict.SetItem(7, "seven");
        Console.WriteLine($"\n{nameof(sortedDict)} trace: ");
        foreach (var item in sortedDict)
        {
            Console.WriteLine($"  {item}");
        }
    }
}