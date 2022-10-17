using Hospital.Integration.Application.Abstractions.Data;
using System.Data;

namespace Hospital.Integration.Infra.Data.Context;

public class UnitOfWork : IUnitOfWork
{
   // private readonly ILogger<UnitOfWork> _logger;
    private int _transactionCounter;
    private bool _disposedValue;

    public UnitOfWork(
       // ILogger<UnitOfWork> logger,
        IDbConnection connection)
    {
        //_logger = logger;

        if (connection.State is not ConnectionState.Open)
        {
            connection.Open();
        }

        Connection = connection;
    }

    public IDbConnection Connection { get; }

    public IDbTransaction Transaction { get; set; }

    public void BeginTransaction()
    {
        if (_transactionCounter == 0)
        {
            Transaction = Connection.BeginTransaction();
        }

        _transactionCounter++;
    }

    public void Commit()
    {
        try
        {
            TryCommit();
        }
        catch (Exception)
        {
            Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        if (Transaction is null)
        {
            return;
        }

        _transactionCounter = 0;
        Transaction.Rollback();
        ClearTransaction();
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
            LogLostedTransaction();

            if (disposing)
            {
                Transaction?.Dispose();
                Connection?.Dispose();
            }

            _disposedValue = true;
        }
    }

    private void TryCommit()
    {
        if (Transaction is null || _transactionCounter < 0)
        {
            return;
        }

        _transactionCounter--;
        if (_transactionCounter > 0)
        {
            return;
        }

        Transaction.Commit();

        ClearTransaction();
    }

    private void ClearTransaction()
    {
        Transaction.Dispose();
        Connection.Close();
    }

    private void LogLostedTransaction()
    {
        if (_transactionCounter == 0)
        {
            return;
        }

        //_logger.LogWarning($"There's a transaction pedding!");
    }
}