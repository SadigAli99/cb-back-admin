

using CB.Application.DTOs.CurrencyRegulationRight;
using FluentValidation;

namespace CB.Application.Validators.CurrencyRegulationRight
{
    public class CurrencyRegulationRightEditValidator : AbstractValidator<CurrencyRegulationRightEditDTO>
    {
        public CurrencyRegulationRightEditValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 100000)
                .WithMessage("Faylın ölçüsü 100 MB-dan çox olmamalıdır.")
                .Must(file =>
                {
                    if (file == null) return true;

                    var allowedTypes = new[]
                    {
                        "application/pdf",
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "application/vnd.ms-excel",
                        "application/msword",
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    };

                    return allowedTypes.Contains(file.ContentType);
                })
                .WithMessage("Fayl yalnız pdf, xlsx və ya doc formatında ola bilər.");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url boş ola bilməz")
                .MaximumLength(500).WithMessage("Url 500 simvoldan artıq ola bilməz.");
        }
    }
}
