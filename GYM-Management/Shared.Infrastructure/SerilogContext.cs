namespace Shared.Infrastructure;

using Serilog.Context;
using Serilog.Core;

public class SerilogContext:ISerilogContext
{


    private Dictionary<string, object?> Properties = new Dictionary<string, object?>();


    public void PushToLogContext(LogColumns logColumn, object? value)
    {
        Properties.Add(logColumn.ToString(), value);
    }

    public ILogEventEnricher GetEnricher()
    {
        foreach (var VARIABLE in Properties)
        {
            LogContext.PushProperty(VARIABLE.Key, VARIABLE.Value);
        }

        return LogContext.Clone();
    }

    public void DisposeContext()
    {
        LogContext.Reset();
        Properties.Clear();
    }



}