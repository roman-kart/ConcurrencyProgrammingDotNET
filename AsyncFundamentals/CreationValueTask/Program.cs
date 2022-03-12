// ValueTask - когда может быть возвращен как асинхронный, так и асинхронный результат
// Лучше использовать Task, но иногда нужно, например, для реализации IAsyncDisposable

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static Dictionary<int, int> calculated = new Dictionary<int, int>()
        {
            {1, 2 },
            {2, 3},
            {3, 4},
            {4, 5},
        };
    public static async Task Main(string[] args)
    {
        Console.WriteLine(await GetIntAsync());

        var digits = new int[] { 1, 2, 3, 5, 5, 6, 6, 7, 7, 7, 7 };
        var tasks = digits.Select(async i =>
        {
            var result = await GetSlowCalc(i);
            Console.WriteLine(result);
        });
        await Task.WhenAll(tasks);
    }
    public static async ValueTask<int> GetIntAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        return 11009;
    }
    // если значение уже было вычислено раньше - возращаем его, в противном случае - вычисляем и кэшируем
    public static async ValueTask<int> GetSlowCalc(int i)
    {
        if (calculated.ContainsKey(i))
        {
            return calculated[i];
        }
        var result = await SlowCalc(i);
        calculated.Add(i, result);
        return result;
    }
    public static async Task<int> SlowCalc(int i)
    {
        await Task.Delay(TimeSpan.FromSeconds(i));
        return ++i;
    }
}