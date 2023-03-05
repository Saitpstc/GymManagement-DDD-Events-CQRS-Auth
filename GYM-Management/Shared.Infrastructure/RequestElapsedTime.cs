namespace Shared.Infrastructure;

using System.Diagnostics;

public class RequestElapsedTime:IRequestElapsedTime
{
    private readonly Stopwatch Stopwatch;

    public RequestElapsedTime()
    {
        Stopwatch = new Stopwatch();
    }

    public void StopAndSaveElapsedTime()
    {
        Stopwatch.Stop();
        ElapsedTime = Stopwatch.Elapsed.TotalMinutes;
    }

    public double GetElapsedTime()
    {
        return ElapsedTime;
    }

    private double ElapsedTime;

    public void StartTimer()
    {
        Stopwatch.Start();

    }
}

public interface IRequestElapsedTime
{
    void StartTimer();

    void StopAndSaveElapsedTime();

    double GetElapsedTime();
}