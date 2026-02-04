

using CB.Application.DTOs.DigitalPaymentInfographic;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.DigitalPaymentInfographic
{
    public class DigitalPaymentInfographicEditValidator : AbstractValidator<DigitalPaymentInfographicEditDTO>
    {
        public DigitalPaymentInfographicEditValidator(
            IDigitalPaymentInfographicCategoryService digitalPaymentCategoryService
        )
        {

            RuleFor(x => x.DigitalPaymentInfographicCategoryId)
                    .MustAsync(async (digitalPaymentCategoryID, cancellation) =>
                    {
                        var digitalPaymentCategory = await digitalPaymentCategoryService.GetByIdAsync(digitalPaymentCategoryID);
                        return digitalPaymentCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Url)
                .MaximumLength(500).WithMessage("Keçid linki 500 simvoldan çox ola bilməz");

        }
    }
}
