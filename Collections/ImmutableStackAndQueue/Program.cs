using System.Collections.Immutable;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        // поток или очередь, которая изменяется не очень часто
        // и к которой можно обращаться из нескольких потоков
        
        // Неизменяемые коллекции по своей природе потокобезопасны,
        // так как любая операция с неизменяемыми коллекциями порождает новую коллекцию
        // (которая использует часть памяти из родительской коллекции)

        // Однако, ПЕРЕМЕННЫЕ НЕИЗМЕНЯМЫХ КОЛЛЕКЦИЙ НУЖДАЮТСЯ В ЗАЩИТЕ КАК И ЛЮБЫЕ ДРУГИЕ ПЕРЕМЕННЫЕ
        ImmutableStack<int> stack = ImmutableStack<int>.Empty;
        stack = stack.Push(101);
        ImmutableStack<int> bigDaddy = stack.Push(1001);

        Console.WriteLine("stack trace: ");
        foreach (var item in stack)
        {
            Console.WriteLine($"  {item}");
        }
        
        Console.WriteLine("bigDaddy trace: ");
        foreach (var item in bigDaddy)
        {
            Console.WriteLine($"  {item}");
        }

        ImmutableQueue<int> queue = ImmutableQueue<int>.Empty;
        queue = queue.Enqueue(10);
        queue = queue.Enqueue(100);
        ImmutableQueue<int> bigMammy = queue.Enqueue(1000);

        Console.WriteLine("\nqueue trace: ");
        foreach (var item in queue)
        {
            Console.WriteLine($"  {item}");
        }

        Console.WriteLine("\nbigMammy trace: ");
        foreach (var item in bigMammy)
        {
            Console.WriteLine($"  {item}");
        }

        int top;
        queue = queue.Dequeue(out top);
        Console.WriteLine($"Dequeue {top} from {nameof(queue)}");
        Console.WriteLine("\nqueue trace: ");
        foreach (var item in queue)
        {
            Console.WriteLine($"  {item}");
        }
    }
}