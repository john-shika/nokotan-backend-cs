using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace NokoWebApiSdk.Cores;

public interface INokoLogger : ILogger
{
    new void Write(LogEventLevel level, string message);
    new void Warning(string message);
    new void Error(string message);
    new void Information(string message);
}

public sealed class NokoLogger : INokoLogger
{
    private static readonly object[] NoPropertyValues = [];
    private static ILogger Logger => Log.Logger;
    
    public void Write(LogEvent logEvent)
    {
        Logger.Write(logEvent);
    }
    
    public void Write(LogEventLevel level, string message)
    {
        Logger.Write(level, message, NoPropertyValues);
    }

    public void Warning(string message)
    {
        Write(LogEventLevel.Warning, message);
    }

    public void Error(string message)
    {
        Write(LogEventLevel.Error, message);
    }

    public void Information(string message)
    {
        Write(LogEventLevel.Information, message);
    }
}