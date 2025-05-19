using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Core.Domain.Entities;
public class Address
{
    public int AddressID { get; set; }
    [StringLength(70)]
    public string AddressName { get; set; } = string.Empty;
}
