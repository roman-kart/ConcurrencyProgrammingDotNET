using System.Collections.Concurrent;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        // словарь, который должен поддерживаться в синхронизованном состоянии, 
        // даже когда с ним работают несколько потоков

        ConcurrentDictionary<int, string> dict = new ConcurrentDictionary<int, string>();

    }
}