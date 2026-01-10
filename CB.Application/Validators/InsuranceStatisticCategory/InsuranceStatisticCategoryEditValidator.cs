using CB.Application.DTOs.InsuranceStatisticCategory;
using FluentValidation;

namespace CB.Application.Validators.InsuranceStatisticCategory
{
    public class InsuranceStatisticCategoryEditValidator : AbstractValidator<InsuranceStatisticCategoryEditDTO>
    {
        public InsuranceStatisticCategoryEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
