namespace Shared.Infrastructure;

using Microsoft.Extensions.Logging;

public class ConsoleLogger:ILogger
{

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }
}

public class DatabaseLoggerProvider:ILoggerProvider
{


    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger();
    }
}

public class DatabaseLogger:ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine("database logger provider test");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return new databaseloggerdisposer();
    }
}

public class MyFactory:ILoggerFactory
{

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ILogger CreateLogger(string categoryName)
    {
        throw new NotImplementedException();
    }

    public void AddProvider(ILoggerProvider provider)
    {
        throw new NotImplementedException();
    }
}

public class databaseloggerdisposer:IDisposable
{

    public void Dispose()
    {
        Console.WriteLine("Database Logger Disposed");
    }
}