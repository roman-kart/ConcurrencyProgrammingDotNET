using RandomThings;

var po = new ParallelOptions();
po.MaxDegreeOfParallelism = Environment.ProcessorCount;

string forSearch = "беб";

List<Action> actions = new List<Action>();
for (int i = 0; i < 100; i++)
{
    actions.Add(() =>
    {
        var words = new string[100];
        words.InsertRandomStrings(10, 1040, 1073);

        for (int j = 0; j < words.Length; j++)
        {
            if (words[j].Contains(forSearch, StringComparison.CurrentCultureIgnoreCase))
            {
                System.Console.WriteLine(words[j]);
            }
        }
        global::System.Console.WriteLine($"Thread number: {Thread.CurrentThread.ManagedThreadId}");
    });
}

Parallel.Invoke(po, actions.ToArray());
Console.ReadKey();