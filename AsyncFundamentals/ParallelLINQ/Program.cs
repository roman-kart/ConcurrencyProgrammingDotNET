namespace InfoAboutTaskExecution;

public sealed class Program
{
    /* PLINQ хуже взаимодействует с системными процессами чем Parallel*/
    public static async Task Main(string[] args)
    {
        var digits = Enumerable.Range(-10_000, 20_002);
        long result = 0;
        foreach (var digit in Multiple2(digits))
        {
            result += digit;
        }
        Console.WriteLine("{0}", result);

        result = 0;
        foreach (var digit in Multiple2Ordered(digits))
        {
            result += digit;
        }
        Console.WriteLine(result);

        Console.WriteLine(digits.AsParallel().Sum());
    }
    public static IEnumerable<int> Multiple2(IEnumerable<int> values)
    {
        return values.AsParallel().Select(i => i * 2); // генерирует значения в любом порядке
    }
    public static IEnumerable<int> Multiple2Ordered(IEnumerable<int> values)
    {
        return values.AsParallel().AsOrdered().Select(i => i * 2); // генерирует значения с сохранением порядка
    }
}