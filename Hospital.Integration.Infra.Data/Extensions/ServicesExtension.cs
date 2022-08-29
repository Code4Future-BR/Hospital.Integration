using Hospital.Integration.Business.Abstractions.Authentication;
using Hospital.Integration.Business.Abstractions.Data;
using Hospital.Integration.Infra.Data.Configuration;
using Hospital.Integration.Infra.Data.Context;
using Hospital.Integration.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Hospital.Integration.Infra.Data.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtension
{
    public static void AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(_ =>
        {
            var connectionString = configuration.GetConnectionString("Default");
            var sqlConnection = new SqlConnectionStringBuilder(connectionString)
            {
                MultipleActiveResultSets = true,
                PoolBlockingPeriod = PoolBlockingPeriod.NeverBlock,
                ConnectRetryCount = 0,
            };

            return new DatabaseConfig()
            {
                ConnectionString = sqlConnection.ConnectionString,
            };
        });
        services.AddTransient<IDbConnection>(provider =>
        {
            var connectionString = provider.GetRequiredService<DatabaseConfig>().ConnectionString;
            return new SqlConnection(connectionString);
        });

        services.AddScoped<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}