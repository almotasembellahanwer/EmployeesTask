using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Core.Domain.Entities;
public class Employee
{
    public int EmployeeID { get; set; }
    [StringLength(40)]
    public string EmployeeName { get; set; } = string.Empty;
    [ForeignKey(nameof(Address))]
    public int? AddressID { get; set; }
    [NotMapped]
    public string? AddressName { get; set; }
    public Address Address { get; set; } = default!;
}
