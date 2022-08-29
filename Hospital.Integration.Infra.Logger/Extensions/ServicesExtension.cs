using Hospital.Integration.Infra.Logger.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Hospital.Integration.Infra.Logger.Extensions;

public static class ServicesExtension
{
    public static void AddLogger(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ILogWriter, LogWriter>();
        services.ConfigLogger(configuration);
    }

    private static void ConfigLogger(
        this IServiceCollection services,
        IConfiguration configuration) => services.AddSingleton(_ =>
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
                .WriteTo.File($"reqsign-{DateTimeOffset.Now}.txt")
                .CreateLogger();

            return Log.Logger;
        });
}
