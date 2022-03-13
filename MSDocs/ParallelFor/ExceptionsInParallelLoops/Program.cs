using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ExceptionsInParallelLoops;

class ExceptionDemo2
{
    static void Main(string[] args)
    {
        // Create some random data to process in parallel.
        // There is a good probability this data will cause some exceptions to be thrown.
        byte[] data = new byte[5000];
        Random r = new Random();
        r.NextBytes(data);

        try
        {
            ProcessDataInParallel(data);
        }
        catch (AggregateException ae)
        {
            var ignoredExceptions = new List<Exception>();
            // This is where you can choose which exceptions to handle.
            foreach (var ex in ae.Flatten().InnerExceptions)
            {
                if (ex is ArgumentException)
                    Console.WriteLine(ex.Message);
                else
                    ignoredExceptions.Add(ex);
            }
            if (ignoredExceptions.Count > 0) throw new AggregateException(ignoredExceptions);
        }


        var words = new string[1_000_000];
        RandomThings.RandomString randomString = new RandomThings.RandomString();
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = randomString.GetString();
        }

        try
        {
            FindForbiddenWord(words);
        }
        catch (AggregateException ae)
        {
            var ignoredExceptions = new List<Exception>();
            foreach (var ex in ae.Flatten().InnerExceptions)
            {
                if (ex is ArgumentException)
                {
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    ignoredExceptions.Add(ex);
                }
            }
            if (ignoredExceptions.Count != 0)
            {
                throw new AggregateException(ignoredExceptions);
            }
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private static void ProcessDataInParallel(byte[] data)
    {
        // Use ConcurrentQueue to enable safe enqueueing from multiple threads.
        var exceptions = new ConcurrentQueue<Exception>();

        // Execute the complete loop and capture all exceptions.
        Parallel.ForEach(data, d =>
        {
            try
            {
                // Cause a few exceptions, but not too many.
                if (d < 3)
                    throw new ArgumentException($"Value is {d}. Value must be greater than or equal to 3.");
                else
                    Console.Write(d + " ");
            }
            // Store the exception and continue with the loop.
            catch (Exception e)
            {
                exceptions.Enqueue(e);
            }
        });
        Console.WriteLine();

        // Throw the exceptions here after the loop completes.
        if (exceptions.Count > 0) throw new AggregateException(exceptions);
    }

    private static void FindForbiddenWord(string[] words)
    {
        var exceptions = new ConcurrentQueue<Exception>();
        string forbiddenWord = "а";

        Parallel.ForEach(words, word =>
        {
            try
            {
                if (word.Contains(forbiddenWord))
                {
                    throw new ArgumentException($"Word {word} contains {forbiddenWord}");
                }
                else
                {
                    //Console.WriteLine(word);
                }
            }
            catch (Exception ex)
            {
                exceptions.Enqueue(ex);
            }
        });
        Console.WriteLine();

        if (exceptions.Count != 0)
        {
            throw new AggregateException(exceptions);
        }
    }
}