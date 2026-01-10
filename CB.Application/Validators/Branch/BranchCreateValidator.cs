using CB.Application.DTOs.Branch;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.Branch
{
    public class BranchCreateValidator : AbstractValidator<BranchCreateDTO>
    {
        public BranchCreateValidator(IDepartmentService departmentService)
        {

            RuleFor(x => x.DepartmentId)
                    .NotNull().WithMessage("Rəhbər şəxsi seçin")
                    .MustAsync(async (departmentID, cancellation) =>
                    {
                        var department = await departmentService.GetByIdAsync(departmentID);
                        return department != null ? true : false;
                    })
                    .WithMessage("Seçilən departament yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
