namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Balance: ");
        decimal userBalance = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Border: ");
        decimal userBorder = Convert.ToDecimal(Console.ReadLine());

        CancellationTokenSource cts = new CancellationTokenSource();
        Progress<decimal> progress = new Progress<decimal>();
        progress.ProgressChanged += (sender, balance) =>
        {
            if (balance < userBorder)
            {
                cts.Cancel();
            }
        };

        Console.WriteLine($"Final balance: {await SpendMyMoney(userBalance, progress, cts.Token)}");
    }
    public static async Task<decimal> SpendMyMoney(decimal balance, IProgress<decimal> progress, CancellationToken cancellationToken)
    {
        decimal spendValue = 100;
        progress?.Report(balance); // оповещение перед выполнением
        while (!cancellationToken.IsCancellationRequested)
        {
            balance -= spendValue;
            Console.WriteLine($"Spend {spendValue}. Current balance: {balance}");
            progress?.Report(balance);
        }
        return balance;
    }
}