
using CB.Application.DTOs.InformationIssuerCaption;
using FluentValidation;

namespace CB.Application.Validators.InformationIssuerCaption
{
    public class InformationIssuerCaptionValidator : AbstractValidator<InformationIssuerCaptionPostDTO>
    {
        public InformationIssuerCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
