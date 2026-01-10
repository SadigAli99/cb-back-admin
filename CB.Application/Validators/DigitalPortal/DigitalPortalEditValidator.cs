

using CB.Application.DTOs.DigitalPortal;
using FluentValidation;

namespace CB.Application.Validators.DigitalPortal
{
    public class DigitalPortalEditValidator : AbstractValidator<DigitalPortalEditDTO>
    {
        public DigitalPortalEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Texts)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Texts)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün mətn 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Url)
                .MaximumLength(500).WithMessage("Keçid linkinin uzunluğu 500 simvoldan çox ola bilməz");
        }
    }
}
