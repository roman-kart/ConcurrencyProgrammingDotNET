Console.WriteLine("Hi, Mark!");

var originalCursorPos = Console.GetCursorPosition();
int waitingTime = 0;
var prodResult = GetDataFromDB.GetProducts();

while (!prodResult.IsCompleted)
{
    Console.SetCursorPosition(originalCursorPos.Left, originalCursorPos.Top);
    Console.WriteLine($"You spended {waitingTime} seconds of your meaningless life :)");
    Thread.Sleep(1000);
    waitingTime++;
}

Console.SetCursorPosition(originalCursorPos.Left, originalCursorPos.Top + 1);
Console.WriteLine("The data has been received successfuly!");
foreach (var prod in prodResult.Result)
{
    Console.WriteLine($"{prod.Name}: {prod.Price}");
}