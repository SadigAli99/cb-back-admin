using CB.Application.DTOs.DigitalPaymentInfograhicCategory;
using FluentValidation;

namespace CB.Application.Validators.DigitalPaymentInfograhicCategory
{
    public class DigitalPaymentInfograhicCategoryCreateValidator : AbstractValidator<DigitalPaymentInfograhicCategoryCreateDTO>
    {
        public DigitalPaymentInfograhicCategoryCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
