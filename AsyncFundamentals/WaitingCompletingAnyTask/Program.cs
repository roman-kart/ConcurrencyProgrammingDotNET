namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var urls = new string[]
        {
            @"https://www.motherfuckingwebsite.com/",
            @"https://linuxtorvalds.com/",
            @"https://www.tinkercad.com/"
        };

        HttpClient client = new HttpClient();
        var downloadHtmlPages = urls.Select(url => GetHtmlPage(client, url));
        var downloadHtmlPageTasks = downloadHtmlPages.ToArray();

        var firstHtmlPageTask = await Task.WhenAny<string>(downloadHtmlPageTasks);
        var firstHtmlPage = await firstHtmlPageTask;
        Console.WriteLine(firstHtmlPage);
    }
    public static async Task<string> GetHtmlPage(HttpClient client, string url)
    {
        return await client.GetStringAsync(url);
    }
}