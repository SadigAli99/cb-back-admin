
using CB.Application.DTOs.Vacancy;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.Vacancy
{
    public class VacancyCreateValidator : AbstractValidator<VacancyCreateDTO>
    {
        public VacancyCreateValidator(
            IDepartmentService departmentService,
            IBranchService branchService,
            IPositionService positionService
            )
        {

            RuleFor(x => x.DepartmentId)
                .NotNull().WithMessage("Departament seçin")
                .MustAsync(async (departmentID, cancellation) =>
                {
                    var department = await departmentService.GetByIdAsync((int)departmentID);
                    return department != null ? true : false;
                })
                .WithMessage("Seçilən departament yanlışdır");

            RuleFor(x => x.BranchId)
                .NotNull().WithMessage("Şöbə seçin")
                .MustAsync(async (branchID, cancellation) =>
                {
                    var branch = await branchService.GetByIdAsync((int)branchID);
                    return branch != null ? true : false;
                })
                .WithMessage("Seçilən şöbə yanlışdır");

            RuleFor(x => x.PositionId)
                .NotNull().WithMessage("Vəzifə seçin")
                .MustAsync(async (positionID, cancellation) =>
                {
                    var position = await positionService.GetByIdAsync((int)positionID);
                    return position != null ? true : false;
                })
                .WithMessage("Seçilən vəzifə yanlışdır");

            RuleFor(x => x.Type)
                .NotNull().WithMessage("Məşğulluq növünü seçin.")
                .IsInEnum().WithMessage("Məşğulluq növü yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Locations)
                .NotEmpty().WithMessage("Ən azı bir dil üçün üvan daxil edilməlidir.");

            RuleForEach(x => x.Locations)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün üvan boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün üvan 500 simvoldan artıq ola bilməz.");
        }
    }
}
