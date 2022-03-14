using System.Collections.Immutable;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        // нужна структура данный, не рассчитанная на хранение дубликатор
        // которая не очень часто изменяется
        // и которая допускает безопасные обращения из нескольких потоков

        // для неизменяемых отсортированных множеств действует таже система,
        // что и для неизменяемых списков - везде foreach вместо for

        ImmutableSortedSet<int> sortedSet = ImmutableSortedSet<int>.Empty;
        sortedSet = sortedSet.Add(101);
        sortedSet = sortedSet.Add(1001);
        ImmutableSortedSet<int> bigGirl = sortedSet;
        bigGirl = bigGirl.Add(100000041);
        bigGirl = bigGirl.Add(100000041);
        bigGirl = bigGirl.Add(100000041);

        Console.WriteLine("\nsortedSet trace: ");
        foreach (var item in sortedSet)
        {
            Console.WriteLine($"  {item}");
        }

        Console.WriteLine("\nbigGirl trace: ");
        foreach (var item in bigGirl)
        {
            Console.WriteLine($"  {item}");
        }

        ImmutableHashSet<int> set = ImmutableHashSet<int>.Empty;
        set = set.Add(123);
        set = set.Add(123);
        set = set.Add(123);

        set = set.Add(321);
        set = set.Add(321);

        set = set.Add(4321);
        set = set.Add(4321);
        set = set.Add(4321);

        Console.WriteLine("\nset trace: ");
        foreach (var item in set)
        {
            Console.WriteLine($"  {item}");
        }
    }
}