using Hospital.Integration.Business.Extensions;
using Hospital.Integration.Infra.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Integration.Infra.Ioc.Extensions;

public static class ServicesExtension
{
    public static void AddIoc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBusiness();
        services.AddData(configuration);
    }
}
