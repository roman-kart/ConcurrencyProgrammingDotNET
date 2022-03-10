using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelFor;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        // завершаем работу приложения, если пользователь ввел не 1 аргумент
        if (args.Length != 1)
        {
            Console.WriteLine("Invalid arguments!");
            Console.WriteLine("Usage: ParallelFor.exe [pathToFolder]");
            return;
        }
        // завершаем приложение, если указанный пользователем каталог не существует
        var path = args[0];
        if (!Directory.Exists(path))
        {
            Console.WriteLine($"{path} doesn't exist!");
            return;
        }
        // находим объем, который занимают все файлы
        long totalSize = 0;
        var files = Directory.GetFiles(path);
        var result = Parallel.For(0, files.Length, (i) =>
        {
            var fileInfo = new FileInfo(files[i]);
            var fileSize = fileInfo.Length;
            Interlocked.Add(ref totalSize, fileSize);
        });

        // выводим сообщение для пользователя на экран
        var originalCursorPosition = Console.GetCursorPosition();
        while (!result.IsCompleted)
        {
            Console.SetCursorPosition(originalCursorPosition.Left, originalCursorPosition.Top);
            Console.WriteLine($"Calculating... {result.LowestBreakIteration}");
        }
        Console.WriteLine($"{path}: {totalSize}");
    }
}