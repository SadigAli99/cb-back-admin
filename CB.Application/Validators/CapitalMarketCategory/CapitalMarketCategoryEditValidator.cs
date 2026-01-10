using CB.Application.DTOs.CapitalMarketCategory;
using FluentValidation;

namespace CB.Application.Validators.CapitalMarketCategory
{
    public class CapitalMarketCategoryEditValidator : AbstractValidator<CapitalMarketCategoryEditDTO>
    {
        public CapitalMarketCategoryEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
