
using CB.Application.DTOs.CurrencyRegulationRightCaption;
using FluentValidation;

namespace CB.Application.Validators.CurrencyRegulationRightCaption
{
    public class CurrencyRegulationRightCaptionValidator : AbstractValidator<CurrencyRegulationRightCaptionPostDTO>
    {
        public CurrencyRegulationRightCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
