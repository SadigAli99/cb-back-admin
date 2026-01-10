using CB.Application.DTOs.ComplaintIndexCategory;
using FluentValidation;

namespace CB.Application.Validators.ComplaintIndexCategory
{
    public class ComplaintIndexCategoryCreateValidator : AbstractValidator<ComplaintIndexCategoryCreateDTO>
    {
        public ComplaintIndexCategoryCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
