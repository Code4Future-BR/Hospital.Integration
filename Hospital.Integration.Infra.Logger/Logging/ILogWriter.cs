using System.Runtime.CompilerServices;

namespace Hospital.Integration.Infra.Logger.Logging;

public interface ILogWriter : IDisposable
{
    void SetCorrelationId(Guid correlationId);

    void Info(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "");

    void Warn(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "");

    void Error(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "");

    void Fatal(
        string message,
        object data = default,
        Exception ex = default,
        [CallerMemberName] string source = "");
}
