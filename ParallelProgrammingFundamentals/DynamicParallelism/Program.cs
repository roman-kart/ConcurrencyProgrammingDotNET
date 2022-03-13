namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {

    }
    public static void Traverse(Node current)
    {
        DoExpensiveWork(current);
        if (current.Left != null)
        {
            Task.Factory.StartNew(
                () => Traverse(current.Left),
                CancellationToken.None,
                TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }
        if (current.Right != null)
        {
            Task.Factory.StartNew(
                () => Traverse(current.Right),
                CancellationToken.None,
                TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
        }
    }
    public static void ProcessTree(Node root)
    {
        /*AttachedProduct - гарантия того, что данная задача привязывается к текущей задаче, то есть создается связь родитель-потомок
         * (родительские задачи выполняют своего делегата, а затем ожидают, когда завершится выполнение дочерних задач)
         * Исключения от дочерних классов распространяются к своей родительской задаче*/
        var task = Task.Factory.StartNew(
            () => Traverse(root),
            CancellationToken.None,
            TaskCreationOptions.AttachedToParent,
            TaskScheduler.Current);
        task.Wait(); // ожидаем завершения выпоплнения корневой задачи, все остальные выполнятся после нее
    }
    public static void DoExpensiveWork(Node node)
    {
        Console.WriteLine(node.Value);
    }
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}