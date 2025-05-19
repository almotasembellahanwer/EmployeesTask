using EmployeeTask.Core.Domain.Entities;

namespace EmployeeTask.Core.Domain.RepositoryContracts;
public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>?> GetAllEmployees();
    Task<Employee?> GetEmployeeByID(int employeeID);
    Task<Employee?> AddEmployee(Employee? entity);
    Task<Employee?> UpdateEmployee(Employee? entity);
    Task<bool> DeleteEmployee(int employeeID);


}
