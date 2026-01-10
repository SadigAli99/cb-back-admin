
using CB.Application.DTOs.ForeignInsuranceBrokerCaption;
using FluentValidation;

namespace CB.Application.Validators.ForeignInsuranceBrokerCaption
{
    public class ForeignInsuranceBrokerCaptionValidator : AbstractValidator<ForeignInsuranceBrokerCaptionPostDTO>
    {
        public ForeignInsuranceBrokerCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
