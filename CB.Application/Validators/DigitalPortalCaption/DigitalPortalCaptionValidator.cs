
using CB.Application.DTOs.DigitalPortalCaption;
using FluentValidation;

namespace CB.Application.Validators.DigitalPortalCaption
{
    public class DigitalPortalCaptionValidator : AbstractValidator<DigitalPortalCaptionPostDTO>
    {
        public DigitalPortalCaptionValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => "Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 1000)
                .WithMessage(v => "Bu dil üçün mətn 1000 simvoldan artıq ola bilməz.");
        }
    }
}
