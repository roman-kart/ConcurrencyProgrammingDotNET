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
        };
        await foreach (var page in GetHtmlPages(urls))
        {
            Console.WriteLine(page.Length);
        }
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