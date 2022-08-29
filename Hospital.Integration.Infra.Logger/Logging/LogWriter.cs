using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;

namespace Hospital.Integration.Infra.Logger.Logging;

public class LogWriter : ILogWriter
{
    private readonly string _appName;
    private ILogger _logger;

    private bool _disposedValue;

    private Guid _correlationId;

    public LogWriter(
        IConfiguration configuration,
        ILogger logger) =>
        (_appName, _logger) = (configuration["AppName"], logger);

    public void SetCorrelationId(Guid correlationId)
    {
        _correlationId = correlationId;
    }

    public void Info(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "")
    {
        Log(message, data, ex, source, LogEventLevel.Information, _logger.Information);
    }

    public void Warn(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "")
    {
        Log(message, data, ex, source, LogEventLevel.Warning, _logger.Warning);
    }

    public void Error(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "")
    {
        Log(message, data, ex, source, LogEventLevel.Error, _logger.Error);
    }

    public void Fatal(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "")
    {
        Log(message, data, ex, source, LogEventLevel.Fatal, _logger.Fatal);
    }

    private void Log(
        string message,
        object data,
        Exception ex,
        string source,
        LogEventLevel level,
        Action<string, LogMessage> logger)
    {
        var correlationId = _correlationId == Guid.Empty ? Guid.NewGuid() : _correlationId;
        var logMessage = new LogMessage
        {
            Application = _appName,
            Data = data,
            Level = level.ToString(),
            Message = message,
            Method = source,
            Timestamp = DateTime.Now,
            CorrelationId = correlationId.ToString(),
        };

        if (ex != null)
        {
            logMessage.StackTrace = $"{ex.Message} {ex.StackTrace}";
        }

        logger.Invoke("{@LogMessage}", logMessage);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _logger = null;
            }

            _disposedValue = true;
        }
    }
}
