using System.Numerics;

var digits = Enumerable.Range(0, 100_000);
long sum = 0;

// Parallel.ForEach<TSource,TLocal>(IEnumerable<TSource>, Func<TLocal>, Func<TSource,ParallelLoopState,TLocal,TLocal>, Action<TLocal>)
Parallel.ForEach(digits, () => 0, (i, loop, subtotal) =>
{
    subtotal += i;
    return subtotal;
},
(subSum) =>
{
    Interlocked.Add(ref sum, subSum);
});

Console.WriteLine($"sum: {sum:N0}");