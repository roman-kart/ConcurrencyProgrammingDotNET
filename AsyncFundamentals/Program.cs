Console.WriteLine("tmp");
TestAsyncFirst testAsyncFirst = new TestAsyncFirst();
var a = testAsyncFirst.DelayResult<int>(12, new TimeSpan(0, 0, 2));

Action<Task<int>> act = (x) =>
{
    var originalConsoleForeground = Console.ForegroundColor; 
    if (!x.IsFaulted)
    {
        Console.ForegroundColor = ConsoleColor.Green;    
        Console.WriteLine("Task successfully complete!");
        Console.WriteLine($"Result: {x.Result}");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Task has completed with error!");
        Console.WriteLine($"Exception message: {x.Exception.Message}");
    }
    Console.ForegroundColor = originalConsoleForeground;
};

//a.ContinueWith(act);

Breakfast.Do();