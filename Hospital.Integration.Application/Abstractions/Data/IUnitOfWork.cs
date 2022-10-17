using System.Data;

namespace Hospital.Integration.Application.Abstractions.Data;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }

    IDbTransaction Transaction { get; }

    void BeginTransaction();

    void Commit();

    void Rollback();
}