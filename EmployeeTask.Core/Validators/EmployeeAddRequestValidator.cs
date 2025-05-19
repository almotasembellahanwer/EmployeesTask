using EmployeeTask.Core.DTO.EmployeeDTO;
using FluentValidation;
namespace EmployeeTask.Core.Validators
{
    public class EmployeeAddRequestValidator : AbstractValidator<EmployeeAddRequest>
    {
        public EmployeeAddRequestValidator()
        {
            RuleFor(temp => temp.EmployeeName)
                .NotEmpty()
                .WithMessage("Employee Name should not be blank")
                .MaximumLength(30)
                .WithMessage("Employee Name should not be greater than 30 character");
        }
    }
}
