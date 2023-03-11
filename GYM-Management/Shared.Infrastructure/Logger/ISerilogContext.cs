namespace Shared.Infrastructure;

using Serilog.Core;

public interface ISerilogContext
{
    void PushToLogContext(LogColumns logColumn, object? value);

    ILogEventEnricher GetEnricher();

    void DisposeContext();
}