using CB.Application.DTOs.BankSectorCategory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.BankSectorCategory
{
    public class BankSectorCategoryEditValidator : AbstractValidator<BankSectorCategoryEditDTO>
    {
        public BankSectorCategoryEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleFor(x => x.Notes)
                .NotEmpty().WithMessage("Ən azı bir dil üçün qeyd daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Notes)
                .Must(v => v.Value.Length <= 1000)
                .WithMessage(v => $"Bu dil üçün qeyd 1000 simvoldan artıq ola bilməz.");
        }
    }
}
