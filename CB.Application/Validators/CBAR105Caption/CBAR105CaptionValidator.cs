
using CB.Application.DTOs.CBAR105Caption;
using FluentValidation;

namespace CB.Application.Validators.CBAR105Caption
{
    public class CBAR105CaptionValidator : AbstractValidator<CBAR105CaptionPostDTO>
    {
        public CBAR105CaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
