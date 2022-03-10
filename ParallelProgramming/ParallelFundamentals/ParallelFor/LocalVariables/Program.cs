using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using RandomThings;

namespace LocalVariables
{
    class Test
    {
        static void Main()
        {
            // получение массива случайно сгенерированных строк
            int arraysCount = 10;
            int wordsCount = 100;
            int wordsLength = 5;
            string[,] words = new string[arraysCount, wordsCount];
            Parallel.For(0, words.GetLength(0), (i) =>
            {
                RandomString randomString = new RandomString();
                for (int j = 0; j < words.GetLength(1); j++)
                {
                    words[i, j] = randomString.GetString(wordsLength, 1073);
                }
            });

            // поиск строки в массивах
            string forFind = "бубра";
            int resultCountOfFind = 0;
            Parallel.For<List<ArrayElement>>(0, words.GetLength(0), () => new List<ArrayElement>(), (i, loop, countOfFind) =>
            {
                for (int j = 0; j < words.GetLength(1); j++)
                {
                    if (String.Compare(forFind, words[i, j], StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        countOfFind.Add(new ArrayElement() { indexes = new int[] { i, j } });
                    }
                }
                return countOfFind;
            },
            (x) =>
            {
                Interlocked.Add(ref resultCountOfFind, x.Count);
                foreach (var word in x)
                {
                    Console.WriteLine(words[word.indexes[0], word.indexes[1]]);
                }
            });
            Console.WriteLine($"{resultCountOfFind} words finded.");

            Parallel.For(0, 10, (i) =>
            {
                int triesCount = 0;
                RandomString randomString = new RandomString();
                string randomWord = randomString.GetString(5, 1073);
                while (String.Compare(forFind, randomWord, StringComparison.CurrentCultureIgnoreCase) != 0 && triesCount < 1_000_000_000)
                {
                    triesCount++;
                    randomWord = randomString.GetString(5, 1073);
                }
                Console.WriteLine($"Count of tries: {triesCount}. Result word: {randomWord}");
            });

            Console.ReadKey();
        }
    }
    public struct ArrayElement
    {
        public int[] indexes;
    }
}