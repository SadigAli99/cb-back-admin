
using CB.Application.DTOs.NonBankCaption;
using FluentValidation;

namespace CB.Application.Validators.NonBankCaption
{
    public class NonBankCaptionValidator : AbstractValidator<NonBankCaptionPostDTO>
    {
        public NonBankCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
