using System.Data;

namespace Hospital.Integration.Infra.Data.Context;

public interface IConnectionFactory
{
    IDbConnection GetNewConnection();
}