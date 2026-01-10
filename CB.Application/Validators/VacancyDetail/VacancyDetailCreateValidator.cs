
using CB.Application.DTOs.VacancyDetail;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.VacancyDetail
{
    public class VacancyDetailCreateValidator : AbstractValidator<VacancyDetailCreateDTO>
    {
        public VacancyDetailCreateValidator(IVacancyService vacancyService)
        {
            RuleFor(x => x.VacancyId)
                    .NotNull().WithMessage("Vakansiyanı şəxsi seçin")
                    .MustAsync(async (vacancyId, cancellation) =>
                    {
                        var vacancy = await vacancyService.GetByIdAsync(vacancyId);
                        return vacancy != null ? true : false;
                    })
                    .WithMessage("Seçilən vakansiya yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
