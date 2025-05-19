using EmployeeTask.Core.Domain.RepositoryContracts;
using EmployeeTask.Core.ServiceContracts;
using EmployeeTask.Core.Services;
using EmployeeTask.Infrastructure.Data;
using EmployeeTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
namespace EmployeeTask.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructures(this IServiceCollection services,IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new ArgumentException("problem with connection string");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        return services;
    }
}
