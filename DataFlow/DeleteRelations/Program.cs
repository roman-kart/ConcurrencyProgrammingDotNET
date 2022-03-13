using System.Threading.Tasks.Dataflow;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var digits = Enumerable.Range(0, 10).ToArray();

        var Multiply2 = new TransformBlock<int, int>(i => i * 2);
        var Multiply3 = new TransformBlock<int, int>(i => i * 3);
        var Divide4 = new TransformBlock<int, int>(i => i / 4);
        var Show = new ActionBlock<int>(i => Console.WriteLine(i));

        var options = new DataflowLinkOptions() { PropagateCompletion = true };

        var mul2Dispose = Multiply2.LinkTo(Multiply3, options);
        var mul3Dispose = Multiply3.LinkTo(Divide4, options);
        var div4Dispose = Divide4.LinkTo(Show, options);

        var middleIndex = digits.Length / 2;
        for (int i = 0; i < middleIndex; i++)
        {
            Multiply2.Post(i);
        }

        await Task.Delay(1000);
        var ShowComments = new ActionBlock<int>(i => Console.WriteLine($"Result: {i}"));
        div4Dispose.Dispose();
        Divide4.LinkTo(ShowComments, options);

        for (int i = middleIndex; i < digits.Length; i++)
        {
            Multiply2.Post(i);
        }
        Multiply2.Complete();
        await ShowComments.Completion;
    }
}