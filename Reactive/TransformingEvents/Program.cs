using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Timers;

namespace InfoAboutTaskExecution;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        var progress = new Progress<int>();
        IObservable<EventPattern<int>> progressReports =
        Observable.FromEventPattern<int>(
        handler => progress.ProgressChanged += handler,
        handler => progress.ProgressChanged -= handler);
        progressReports.Subscribe(data => Trace.WriteLine("OnNex" 
            + "data.EventArgs"));

        var timer = new System.Timers.Timer(interval: 1000) { Enabled = true };
        IObservable<EventPattern<ElapsedEventArgs>> ticks =
        Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(
        handler => (s, a) => handler(s, a),
        handler => timer.Elapsed += handler,
        handler => timer.Elapsed -= handler);
        ticks.Subscribe(data => Trace.WriteLine("OnNext"
         + "data.EventArgs.SignalTime"));

        var timer1 = new System.Timers.Timer(interval: 1000) { Enabled = true };
        IObservable<EventPattern<object>> ticks1 =
        Observable.FromEventPattern(timer1, nameof(System.Timers.Timer.Elapsed));
        ticks.Subscribe(data => Trace.WriteLine("OnNext: "
        + ((ElapsedEventArgs)data.EventArgs).SignalTime));
    }
}