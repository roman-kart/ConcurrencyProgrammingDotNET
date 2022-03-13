using System.Threading.Tasks.Dataflow;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        DataflowLinkOptions dataflowLinkOptions = new DataflowLinkOptions() { PropagateCompletion = true};

        var sourceBlock = new BufferBlock<int>();
        var options = new DataflowBlockOptions() { BoundedCapacity = 2 };
        var targetBlockA = new BufferBlock<int>(options);
        var targetBlockB = new BufferBlock<int>(options);

        sourceBlock.LinkTo(targetBlockA, dataflowLinkOptions);
        sourceBlock.LinkTo(targetBlockB, dataflowLinkOptions);

        var showDataA = new ActionBlock<int>(i => Console.WriteLine($"A: {i}"));
        var showDataB = new ActionBlock<int>(i => Console.WriteLine($"B: {i}"));
        targetBlockA.LinkTo(showDataA, dataflowLinkOptions);
        targetBlockB.LinkTo(showDataB, dataflowLinkOptions);

        var digits = Enumerable.Range(0, 10).ToArray();
        foreach (var digit in digits)
        {
            sourceBlock.Post(digit);
        }
        sourceBlock.Complete();
        await showDataA.Completion;
        await showDataB.Completion;
    }
}