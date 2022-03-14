using System.Collections.Immutable;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        // нужна структура с возможностью индексирования, 
        // которая изменяется не слишком часто
        // и допускает безопасные обращения из нескольких потоков

        // для реализации неизменяемого списка используется двоичное дерево

        // обращение по индексу - logN, поэтому везде foreeach вместо for где это возможно

        ImmutableList<int> list = ImmutableList<int>.Empty;
        list = list.Add(1);
        list = list.Add(2);
        list = list.Add(3);
        ImmutableList<int> bigBro = list;
        bigBro = bigBro.Add(112233);
        bigBro = bigBro.Add(1890);


        Console.WriteLine("\nlist trace: ");
        foreach (var item in list)
        {
            Console.WriteLine($"  {item}");
        }

        Console.WriteLine("\nbigBro trace: ");
        foreach (var item in bigBro)
        {
            Console.WriteLine($"  {item}");
        }
    }
}