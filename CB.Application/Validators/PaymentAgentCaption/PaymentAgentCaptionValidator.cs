
using CB.Application.DTOs.PaymentAgentCaption;
using FluentValidation;

namespace CB.Application.Validators.PaymentAgentCaption
{
    public class PaymentAgentCaptionValidator : AbstractValidator<PaymentAgentCaptionPostDTO>
    {
        public PaymentAgentCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
