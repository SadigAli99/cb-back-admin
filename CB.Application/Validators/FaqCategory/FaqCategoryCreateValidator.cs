using CB.Application.DTOs.FaqCategory;
using FluentValidation;

namespace CB.Application.Validators.FaqCategory
{
    public class FaqCategoryCreateValidator : AbstractValidator<FaqCategoryCreateDTO>
    {
        public FaqCategoryCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
