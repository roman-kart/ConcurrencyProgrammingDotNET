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
    }
    public static async Task<string[]> GetHtmlPages(HttpClient httpClient, string[] urls)
    {

    } 
}