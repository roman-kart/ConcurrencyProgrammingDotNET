// ValueTask и ValueTask<T> можно ожидать только 1 раз

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        /*Отличия ValueTask от Task:
         * 1) Получить результат ValueTask можно только 1 раз
         */
        var digits = Enumerable.Range(0, 5).ToArray();
        var valueTasks = digits.Select(i => GetInt(i));

        // первый способ - привести к Task и обработать как обычную задачу
        // AsTask() можно только 1 раз!
        var tasks = valueTasks.Select(vt => vt.AsTask()).ToArray();
        var results = await Task.WhenAll(tasks);
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }

        // второй способ - сразу получить результат при помощи await
        var processingVT = digits.Select(async i =>
        {
            var result = await GetInt(i);
            Console.WriteLine(result);
        });

        await Task.WhenAll(processingVT);
    }
    public static async ValueTask<int> GetInt(int i)
    {
        await Task.Delay(TimeSpan.FromSeconds(i));
        return i;
    }
}