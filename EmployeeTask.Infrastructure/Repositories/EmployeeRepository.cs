using AutoMapper;
using EmployeeTask.Core.Domain.Entities;
using EmployeeTask.Core.Domain.RepositoryContracts;
using EmployeeTask.Core.DTO.EmployeeDTO;
using EmployeeTask.Core.Exceptions;
using EmployeeTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Infrastructure.Repositories;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Employee>?> GetAllEmployees()
    {
        IEnumerable<Employee> employees = await _context.Employees
            .Include(a=>a.Address)
            .ToListAsync();
        if (employees is null)
            return new List<Employee>();
        return employees;
    }

    public async Task<Employee?> GetEmployeeByID(int employeeID)
    {
        if (employeeID == 0)
            throw new BadRequestException($"Invalid ID");

        EmployeeResponse? employeeResponse = await (from e in _context.Employees
                    join a in _context.Addresses on e.AddressID equals a.AddressID into addressGroup
                    from address in addressGroup.DefaultIfEmpty()
                    where e.EmployeeID == employeeID
                    select new EmployeeResponse(e.EmployeeID, e.EmployeeName, address.AddressName))
                    .FirstOrDefaultAsync();


        if (employeeResponse is null)
            throw new NotFoundException("Employee not found");
        Employee? employee = _mapper.Map<Employee>(employeeResponse);
        return employee;
    }
    public async Task<Employee?> AddEmployee(Employee? entity)
    {
        if (entity is null)
            throw new ArgumentNullException("Invalid Employee");
         _context.Employees.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Employee?> UpdateEmployee(Employee? entity)
    {
        if (entity is null)
            throw new ArgumentNullException("Invalid Employee");
        _context.Employees.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteEmployee(int employeeID)
    {
        Employee? employee = await GetEmployeeByID(employeeID);
        if (employee is null)
            throw new NotFoundException("Employee not found");
        _context.Employees.Remove(employee);
        int rowsCountAffected = await _context.SaveChangesAsync();
        return rowsCountAffected > 0;
    }
}
