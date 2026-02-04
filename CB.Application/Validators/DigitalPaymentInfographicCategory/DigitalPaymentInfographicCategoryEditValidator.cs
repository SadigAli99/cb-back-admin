using CB.Application.DTOs.DigitalPaymentInfographicCategory;
using FluentValidation;

namespace CB.Application.Validators.DigitalPaymentInfographicCategory
{
    public class DigitalPaymentInfographicCategoryEditValidator : AbstractValidator<DigitalPaymentInfographicCategoryEditDTO>
    {
        public DigitalPaymentInfographicCategoryEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
