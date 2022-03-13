using System.Threading.Tasks.Dataflow;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var multiplyBlock = new TransformBlock<int, int>(
            i => i * 2,
            new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded });
        var showItems = new ActionBlock<int>(
            i => Console.WriteLine(i),
            new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded});
        multiplyBlock.LinkTo(showItems, new DataflowLinkOptions { PropagateCompletion = true});

        foreach (var digit in Enumerable.Range(0, 10).ToArray())
        {
            multiplyBlock.Post(digit);
        }
        multiplyBlock.Complete();
        await showItems.Completion;
    }
}