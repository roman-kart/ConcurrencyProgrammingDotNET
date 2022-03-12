/*
 * Когда async-метод возобновляет работу после await, по умолчанию он
 * продолжает выполнение в том же контексте. Это может создать проблемы
 * с быстродействием, если контекстом был UI-контекст, а в UI-контексте
 * возобновляет работу большое количество async-методов.
 */

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {

    }
    // возобновляется в том же контексте
    public static async Task ResumeOnContextAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
    // не возобновляет контекст при выполении
    public static async Task ResumeWithoutDefaultContext()
    {
        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
    }
}