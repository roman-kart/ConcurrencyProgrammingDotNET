namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var urls = new string[]
        {
            "https://yandex.ru/",
            "https://www.w3schools.com/html/html_basic.asp",
            "https://www.amazon.com/",
            "https://www.toronto.ca/",
            "https://www.dpd.co.uk/",
            "https://www.ups.com/gb/en/Home.page",
            "https://www.airchina.ru/RU/CN/Home",
            "https://www.csair.com/cn/index.shtml",
            "http://gofman39.narod.ru/"
        };

        /* В NuGET-пакете System.Linq.Async находятся методы для работы с IAsyncEnumerable
         * предусмотрены методы с модификаторами Async и Await:
         * Async - возвращает задачу, которая возвращает результат, принимает синхронный делегат
         * Await - принимает асинхронный делегат
         */

        var finalPages = GetHtmlPages(urls).WhereAwait(async page =>
        {
            return page.Length < 12_000;
        });

        await foreach (var page in finalPages)
        {
            Console.WriteLine(page.Length);
        }

        var countTask = GetHtmlPages(urls).CountAsync(page =>
        {
            return page.Length > 12_000;
        });
        Console.WriteLine($"Count: {await countTask}");
    }
    public static async IAsyncEnumerable<string> GetHtmlPages(string[] urls)
    {
        HttpClient client = new HttpClient();
        foreach (var url in urls)
        {
            yield return await client.GetStringAsync(url);
        }
    }
}