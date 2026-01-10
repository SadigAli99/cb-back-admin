
using CB.Application.DTOs.FraudStatisticCaption;
using FluentValidation;

namespace CB.Application.Validators.FraudStatisticCaption
{
    public class FraudStatisticCaptionValidator : AbstractValidator<FraudStatisticCaptionPostDTO>
    {
        public FraudStatisticCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
