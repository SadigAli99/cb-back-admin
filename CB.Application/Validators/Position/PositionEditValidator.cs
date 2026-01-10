using CB.Application.DTOs.Position;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.Position
{
    public class PositionEditValidator : AbstractValidator<PositionEditDTO>
    {
        public PositionEditValidator(IBranchService branchService)
        {

            RuleFor(x => x.BranchId)
                    .MustAsync(async (branchID, cancellation) =>
                    {
                        var branch = await branchService.GetByIdAsync((int)branchID);
                        return branch != null ? true : false;
                    })
                    .WithMessage("Seçilən departament yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
