

using CB.Application.DTOs.InfographicDisclosure;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.InfographicDisclosure
{
    public class InfographicDisclosureEditValidator : AbstractValidator<InfographicDisclosureEditDTO>
    {
        public InfographicDisclosureEditValidator(
            IInfographicDisclosureCategoryService infographicDisclosureCategoryService,
            IInfographicDisclosureFrequencyService infographicDisclosureFrequencyService
        )
        {

            RuleFor(x => x.InfographicDisclosureCategoryId)
                    .MustAsync(async (infographicDisclosureCategoryID, cancellation) =>
                    {
                        var infographicDisclosureCategory = await infographicDisclosureCategoryService.GetByIdAsync(infographicDisclosureCategoryID);
                        return infographicDisclosureCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.InfographicDisclosureFrequencyId)
                    .MustAsync(async (infographicDisclosureFrequencyID, cancellation) =>
                    {
                        var infographicDisclosureFrequency = await infographicDisclosureFrequencyService.GetByIdAsync(infographicDisclosureFrequencyID);
                        return infographicDisclosureFrequency != null ? true : false;
                    })
                    .WithMessage("Seçilən dövrilik yanlışdır");

            RuleFor(x => x.Deadlines)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Deadlines)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");

        }
    }
}
