using System.Collections.Concurrent;

namespace MyWebApplication.Logging;

public class CustomLoggerProvider(CustomLoggerProviderConfiguration configuration) : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, CustomLogger> _loggers =
        new ConcurrentDictionary<string, CustomLogger>();

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new CustomLogger(name, configuration));
    }

    public void Dispose()
    {
        _loggers.Clear();
    }
}