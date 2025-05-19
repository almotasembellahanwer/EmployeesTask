using EmployeeTask.Core.ServiceContracts;
using EmployeeTask.Core.Services;
using Microsoft.Extensions.DependencyInjection;
namespace EmployeeTask.Core;
public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<IAddressesService, AddressesService>();

        return services;
    }
}
