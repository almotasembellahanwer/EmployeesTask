using EmployeeTask.Core.DTO.EmployeeDTO;
namespace EmployeeTask.Core.ServiceContracts;
public interface IEmployeesService
{
    Task<IEnumerable<EmployeeResponse>?> GetAllEmployees();
    Task<EmployeeResponse?> GetEmployeeByID(int employeeID);
    Task<EmployeeResponse?> AddEmployee(EmployeeAddRequest? entity);
    Task<EmployeeResponse?> UpdateEmployee(EmployeeUpdateRequest? entity);
    Task<bool> DeleteEmployee(int employeeID);
}
