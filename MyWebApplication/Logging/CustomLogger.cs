namespace MyWebApplication.Logging;

public class CustomLogger(string name, CustomLoggerProviderConfiguration configuration) : ILogger
{

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= configuration.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        WriteLogInFile(message);
    }

    private void WriteLogInFile(string message)
    {
        var path = "/home/leviferreira/random-docs/Logs.txt";
        using var streamWriter = new StreamWriter(path, true);
        streamWriter.WriteLine(message);
        streamWriter.Close();
    }
}