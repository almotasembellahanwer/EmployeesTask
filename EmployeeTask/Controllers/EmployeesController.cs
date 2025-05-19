using EmployeeTask.Core.DTO;
using EmployeeTask.Core.DTO.EmployeeDTO;
using EmployeeTask.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _response = new();
            _employeesService = employeesService;
        }
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllEmployees()
        {
            IEnumerable<EmployeeResponse>? employees = await _employeesService.GetAllEmployees();
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = employees;
            return Ok(_response);
        }
        [HttpGet("Get/{employeeID:int}",Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEmployee(int employeeID)
        {
            if (employeeID == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            EmployeeResponse? employee = await _employeesService.GetEmployeeByID(employeeID);
            if (employee is null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = employee;
            return Ok(_response);
        }
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> AddEmployee(EmployeeAddRequest? employeeRequest)
        {
            if (employeeRequest is null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            EmployeeResponse? employee = await _employeesService.AddEmployee(employeeRequest);
            if (employee is null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = employee;
            return CreatedAtRoute("GetEmployee", new { employeeID = employee.EmployeeID }, _response);
        }
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(EmployeeUpdateRequest? employeeRequest)
        {
            if (employeeRequest is null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            EmployeeResponse? employee = await _employeesService.UpdateEmployee(employeeRequest);
            if (employee is null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = employee;
            return Ok(_response);
        }
        [HttpDelete("Delete/{employeeID:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteEmployee(int employeeID)
        {
            if (employeeID == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            bool isDeleted = await _employeesService.DeleteEmployee(employeeID);
            if (!isDeleted)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            return NoContent();
        }
    }
}
