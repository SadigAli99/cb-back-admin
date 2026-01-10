using CB.Application.DTOs.StaffArticle;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.StaffArticle
{
    public class StaffArticleEditValidator : AbstractValidator<StaffArticleEditDTO>
    {
        public StaffArticleEditValidator()
        {
            RuleFor(x => x.Year)
                    .GreaterThanOrEqualTo(1900).WithMessage("İl dəyəri 1900-dan kiçik ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleFor(x => x.SubTitles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün alt başlıq daxil edilməlidir.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ünvan daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => $"Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => $"Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.SubTitles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => $"Bu dil üçün alt başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => $"Bu dil üçün alt başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => $"Bu dil üçün ünvan boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => $"Bu dil üçün ünvan 500 simvoldan artıq ola bilməz.");
        }
    }
}
