
using CB.Application.DTOs.MissionCaption;
using FluentValidation;

namespace CB.Application.Validators.MissionCaption
{
    public class MissionCaptionValidator : AbstractValidator<MissionCaptionPostDTO>
    {
        public MissionCaptionValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => "Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 10000)
                .WithMessage(v => "Bu dil üçün mətn 10000 simvoldan artıq ola bilməz.");
        }
    }
}
