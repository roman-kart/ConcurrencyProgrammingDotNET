namespace WaitingComletingGroupOfTasks;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        Task<string> taskA = Task.FromResult<string>("TaskA");
        Task<string> taskB = Task.FromResult<string>("TaskB");
        Task<string> taskC = Task.FromResult<string>("TaskC");
        Task<string> taskE = Task.FromException<string>(new Exception("Hah, exception!"));

        string[] result = null;
        try
        {
            result = await Task.WhenAll<string>(taskA, taskB, taskC);

            foreach (var str in result)
            {
                Console.WriteLine(str);
            }

            result = await Task.WhenAll<string>(taskA, taskB, taskC, taskE);

            foreach (var str in result)
            {
                Console.WriteLine(str);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(result == null);
        }

        // Ожидание завершения группы задач
        HttpClient httpClient = new HttpClient();
        var urls = new string[]
        {
            "https://www.youtube.com/watch?v=uuBETyA_yxc",
            "https://github.com/roman-kart/CLR_via_CSharp/tree/main/Delegates",
            "https://www.google.com/",
        };
        var htmlPagesTask = GetHtmlPages(httpClient, urls);
        try
        {
            var htmlPages = await htmlPagesTask;
            foreach (var page in htmlPages)
            {
                Console.WriteLine(page.Length);
            }
        }
        catch (HttpRequestException re)
        {
            Console.WriteLine(re.Message);
        }
    }
    public static async Task<string[]> GetHtmlPages(HttpClient client, string[] urls)
    {
        var downloads = urls.Select(url => client.GetStringAsync(url));

        Console.WriteLine("Before executing");
        var downloadTasks = downloads.ToArray();
        Console.WriteLine("After executing");

        var htmlPages = await Task.WhenAll(downloadTasks);
        
        return htmlPages;
    } 
}