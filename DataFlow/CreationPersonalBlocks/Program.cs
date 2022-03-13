using System.Threading.Tasks.Dataflow;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var custom = CreateMyCustomBlock();
        var show = new ActionBlock<int>(i => Console.WriteLine(i));
        custom.LinkTo(show, new DataflowLinkOptions { PropagateCompletion = true });
        foreach (var digit in Enumerable.Range(0, 10))
        {
            custom.Post(digit);
        }
        custom.Complete();
        await show.Completion;
    }
    static IPropagatorBlock<int, int> CreateMyCustomBlock()
    {
        var multiplyBlock = new TransformBlock<int, int>(item => item * 2);
        var addBlock = new TransformBlock<int, int>(item => item + 2);
        var divideBlock = new TransformBlock<int, int>(item => item / 2);
        var flowCompletion = new DataflowLinkOptions { PropagateCompletion = true };
        multiplyBlock.LinkTo(addBlock, flowCompletion);
        addBlock.LinkTo(divideBlock, flowCompletion);
        return DataflowBlock.Encapsulate(multiplyBlock, divideBlock);
    }
}