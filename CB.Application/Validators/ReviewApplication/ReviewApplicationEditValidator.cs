using CB.Application.DTOs.ReviewApplication;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.ReviewApplication
{
    public class ReviewApplicationEditValidator : AbstractValidator<ReviewApplicationEditDTO>
    {
        public ReviewApplicationEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => $"Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => $"Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)

                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => $"Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
