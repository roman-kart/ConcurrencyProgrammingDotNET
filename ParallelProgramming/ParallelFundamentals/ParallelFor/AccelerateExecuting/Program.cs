using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using RandomThings;

class Program
{
    static void Main()
    {

        // Source must be array or IList.
        var source = Enumerable.Range(0, 1_000_000).ToArray();

        // Partition the entire source array.
        var rangePartitioner = Partitioner.Create(0, source.Length);

        double[] results = new double[source.Length];

        // Loop over the partitions in parallel.
        Parallel.ForEach(rangePartitioner, (range, loopState) =>
        {
            // Loop over each range element without a delegate invocation.
            for (int i = range.Item1; i < range.Item2; i++)
            {
                results[i] = source[i] * Math.PI;
            }
        });

        Console.WriteLine("Operation complete. Print results? y/n");
        char input = Console.ReadKey().KeyChar;
        if (input == 'y' || input == 'Y')
        {
            foreach (double d in results)
            {
                Console.Write("{0} ", d);
            }
        }

        string forSearch = "хек";
        var findedWords = new List<string>();
        var words = new string[1_000_000];
        words.InsertRandomStrings();

        var wordsPartitioner = Partitioner.Create(0, words.Length);

        Parallel.ForEach(wordsPartitioner, (range, loopState) =>
        {
            for (int i = range.Item1; i < range.Item2; i++)
            {
                if (words[i].Contains(forSearch, StringComparison.CurrentCultureIgnoreCase))
                {
                    findedWords.Add(words[i]);
                }
            }
        });

        foreach (var word in findedWords)
        {
            Console.WriteLine(word);
        }
    }
}