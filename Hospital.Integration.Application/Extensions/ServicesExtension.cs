
using Hospital.Integration.Application.QueriesHandlers.Common;
using Hospital.Integration.Application.QueriesHandlers.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Integration.Application.Extensions;

public static class ServicesExtension
{
    public static void AddApllication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatRApi();
    }

    public static void AddMediatRApi(this IServiceCollection services)
    {
        // services.AddMediatR(typeof(DepartmentByIdQuery));
        // services.AddMediatR(typeof(DepartmentsQuery));
        services.AddMediatR(typeof(UserValidateCredentialQuery));
    }
}
