using CB.Application.DTOs.NominationCategory;
using FluentValidation;

namespace CB.Application.Validators.NominationCategory
{
    public class NominationCategoryCreateValidator : AbstractValidator<NominationCategoryCreateDTO>
    {
        public NominationCategoryCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
