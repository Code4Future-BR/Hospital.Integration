using Hospital.Integration.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Integration.Business.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddBusiness(this IServiceCollection services) => services
        .AddScoped<IAuthService, AuthService>();
}