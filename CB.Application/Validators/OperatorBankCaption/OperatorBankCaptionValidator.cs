
using CB.Application.DTOs.OperatorBankCaption;
using FluentValidation;

namespace CB.Application.Validators.OperatorBankCaption
{
    public class OperatorBankCaptionValidator : AbstractValidator<OperatorBankCaptionPostDTO>
    {
        public OperatorBankCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
