using CB.Application.DTOs.CapitalMarketCategory;
using FluentValidation;

namespace CB.Application.Validators.CapitalMarketCategory
{
    public class CapitalMarketCategoryCreateValidator : AbstractValidator<CapitalMarketCategoryCreateDTO>
    {
        public CapitalMarketCategoryCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
