using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Hospital.Integration.Infra.Data.Configuration;

namespace Hospital.Integration.Infra.Data.Context;

[ExcludeFromCodeCoverage]
public class ConnectionFactory : IConnectionFactory
{
    private readonly string? _connectionString;

    public ConnectionFactory(DatabaseConfig database) =>
        _connectionString = database.ConnectionString;

    public IDbConnection GetNewConnection() =>
        new SqlConnection(_connectionString);
}