using CB.Application.DTOs.PercentCorridorCategory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.PercentCorridorCategory
{
    public class PercentCorridorCategoryEditValidator : AbstractValidator<PercentCorridorCategoryEditDTO>
    {
        public PercentCorridorCategoryEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleFor(x => x.Notes)
                .NotEmpty().WithMessage("Ən azı bir dil üçün qeyd daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 100)
                .WithMessage(v => $"Bu dil üçün başlıq 100 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Notes)
                .Must(v => v.Value.Length <= 1000)
                .WithMessage(v => $"Bu dil üçün qeyd 1000 simvoldan artıq ola bilməz.");
        }
    }
}
