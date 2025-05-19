using EmployeeTask.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }

}
