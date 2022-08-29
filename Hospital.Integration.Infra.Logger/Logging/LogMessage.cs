namespace Hospital.Integration.Infra.Logger.Logging;

public class LogMessage
{
    public DateTime Timestamp { get; init; }

    public string Level { get; init; }

    public string Message { get; init; }

    public string CorrelationId { get; init; }

    public object Data { get; init; }

    public string Application { get; init; }

    public string Method { get; init; }

    public string StackTrace { get; set; }
}
