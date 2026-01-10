using CB.Application.DTOs.InfographicDisclosureFrequency;
using FluentValidation;

namespace CB.Application.Validators.InfographicDisclosureFrequency
{
    public class InfographicDisclosureFrequencyEditValidator : AbstractValidator<InfographicDisclosureFrequencyEditDTO>
    {
        public InfographicDisclosureFrequencyEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
