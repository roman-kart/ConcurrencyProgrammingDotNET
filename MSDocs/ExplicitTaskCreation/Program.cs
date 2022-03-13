Thread.CurrentThread.Name = "zaTASKanniy";

// если необходимо контролировать выполнение задачи, можно явно создать ее при помощи конструктора класса Task
Task taskA = new Task(() =>
{
    Console.WriteLine($"Task thread name: {Thread.CurrentThread.Name}");
});

taskA.Start();

Console.WriteLine($"Main thread name: {Thread.CurrentThread.Name}");

taskA.Wait();