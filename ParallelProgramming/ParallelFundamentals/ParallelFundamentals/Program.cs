using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

Stopwatch stopwatch = Stopwatch.StartNew();

int lastDigit = 1000;
long result = 0;

// нет выигрыша, так как операции несложные

stopwatch.Restart();
for (int i = 0; i < lastDigit; i++)
{
    result += i;
}
stopwatch.Stop();
Console.WriteLine($"WithoutParallel: Result: {result}, Time: {stopwatch.ElapsedMilliseconds}.");

result = 0;
stopwatch.Restart();
Parallel.For(0, lastDigit, (i) =>
{
    result += i;
});
stopwatch.Stop();
Console.WriteLine($"WithParallel: Result: {result}, Time: {stopwatch.ElapsedMilliseconds}.");



// более сложные операции (сортировка нескольких массивов)
int countOfArrays = 15;
List<int[]> arrays = new List<int[]>();
List<int[]> arraysWith = new List<int[]>();
List<int[]> arraysWithout = new List<int[]>();
for (int i = 0; i < countOfArrays; i++)
{
    int countOfElementsInCurrentArray = RandomNumberGenerator.GetInt32(1_000_000, 10_000_000);
    arrays.Add(new int[countOfElementsInCurrentArray]);
    arraysWith.Add(new int[countOfElementsInCurrentArray]);
    arraysWithout.Add(new int[countOfElementsInCurrentArray]);
    for (int j = 0; j < countOfElementsInCurrentArray; j++)
    {
        var digit = RandomNumberGenerator.GetInt32(-1000, 1000);
        arrays[i][j] = digit;
        arraysWith[i][j] = digit;
        arraysWithout[i][j] = digit;
    }
}

// непаралельная сортировка
Console.WriteLine("Sorting");
stopwatch.Restart();
for (int i = 0; i < arraysWithout.Count; i++)
{
    Array.Sort<int>(arraysWithout[i]);
}
stopwatch.Stop();
Console.WriteLine($"WithoutParallel: Time: {stopwatch.ElapsedMilliseconds}."); // 2631

// параллельная
stopwatch.Restart();
Parallel.For(0, arraysWith.Count, (i) =>
{
    Array.Sort(arraysWith[i]);
});
stopwatch.Stop();
Console.WriteLine($"WithParallel: Time: {stopwatch.ElapsedMilliseconds}."); // 235