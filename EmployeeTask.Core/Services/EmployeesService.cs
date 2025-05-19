using AutoMapper;
using EmployeeTask.Core.Domain.Entities;
using EmployeeTask.Core.Domain.RepositoryContracts;
using EmployeeTask.Core.DTO.EmployeeDTO;
using EmployeeTask.Core.Exceptions;
using EmployeeTask.Core.ServiceContracts;
using System.Collections.Generic;
namespace EmployeeTask.Core.Services;
public class EmployeesService : IEmployeesService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeesService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeResponse>?> GetAllEmployees()
    {
        IEnumerable<Employee>? employees = await _employeeRepository.GetAllEmployees();
        if (employees is null)
            return new List<EmployeeResponse>();
        IEnumerable<EmployeeResponse> response = _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
        return response;
    }

    public async Task<EmployeeResponse?> GetEmployeeByID(int employeeID)
    {
        if (employeeID == 0)
            throw new BadRequestException("Invalid ID");
        Employee? employee = await _employeeRepository.GetEmployeeByID(employeeID);
        if (employee is null)
            throw new NotFoundException("employee not found");
        EmployeeResponse response = _mapper.Map<EmployeeResponse>(employee);
        return response;
    }
    public async Task<EmployeeResponse?> AddEmployee(EmployeeAddRequest? entity)
    {
        if (entity is null)
            throw new BadRequestException("Invalid employee to add");
        Employee employee = _mapper.Map<Employee>(entity);
        Employee? employeeAdded = await _employeeRepository.AddEmployee(employee);
        if (employeeAdded is null)
            throw new BadRequestException("error while adding employee");
        EmployeeResponse response = _mapper.Map<EmployeeResponse>(employeeAdded);
        return response;
    }
    public async Task<EmployeeResponse?> UpdateEmployee(EmployeeUpdateRequest? entity)
    {
        if (entity is null)
            throw new BadRequestException("Invalid employee to add");
        Employee employee = _mapper.Map<Employee>(entity);
        Employee? employeeUpdated = await _employeeRepository.UpdateEmployee(employee);
        if (employeeUpdated is null)
            throw new BadRequestException("error while updating employee");
        EmployeeResponse response = _mapper.Map<EmployeeResponse>(employeeUpdated);
        return response;
    }
    public async Task<bool> DeleteEmployee(int employeeID)
    {
        if (employeeID == 0)
            throw new BadRequestException("Invalid ID");
        bool isDeleted = await _employeeRepository.DeleteEmployee(employeeID);
        return isDeleted;
    }
}
