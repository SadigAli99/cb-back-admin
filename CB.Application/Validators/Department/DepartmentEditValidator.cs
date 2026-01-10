using CB.Application.DTOs.Department;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.Department
{
    public class DepartmentEditValidator : AbstractValidator<DepartmentEditDTO>
    {
        public DepartmentEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
