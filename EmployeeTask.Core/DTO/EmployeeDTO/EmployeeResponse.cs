namespace EmployeeTask.Core.DTO.EmployeeDTO;
public record EmployeeResponse(int? EmployeeID, string? EmployeeName, string? AddressName)
{
    public EmployeeResponse() : this(default,default,default)
    {
        
    }
}
