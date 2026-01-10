
using CB.Application.DTOs.InstantPaymentPost;
using FluentValidation;

namespace CB.Application.Validators.InstantPaymentPost
{
    public class InstantPaymentPostCreateValidator : AbstractValidator<InstantPaymentPostCreateDTO>
    {
        public InstantPaymentPostCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.ShortDescriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün qısa mətn daxil edilməlidir.");

            RuleForEach(x => x.ShortDescriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün qısa mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 1000)
                .WithMessage("Bu dil üçün qısa mətn 1000 simvoldan artıq ola bilməz.");

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
