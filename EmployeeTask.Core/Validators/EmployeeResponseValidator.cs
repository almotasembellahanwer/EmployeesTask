using EmployeeTask.Core.DTO.EmployeeDTO;
using FluentValidation;
namespace EmployeeTask.Core.Validators
{
    public class EmployeeResponseValidator : AbstractValidator<EmployeeResponse>
    {
        public EmployeeResponseValidator()
        {
            RuleFor(temp => temp.EmployeeID)
                .NotEmpty()
                .WithMessage("Employee ID should not be blank");
            RuleFor(temp => temp.EmployeeName)
                .NotEmpty()
                .WithMessage("Employee Name should not be blank")
                .MaximumLength(30)
                .WithMessage("Employee Name should not be greater than 30 character");
            RuleFor(temp => temp.AddressName)
                .MaximumLength(70)
                .WithMessage("Address Name should not be greater than 70 character");
        }
    }
}
