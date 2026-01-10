
using CB.Application.DTOs.FaqVideo;
using FluentValidation;

namespace CB.Application.Validators.FaqVideo
{
    public class FaqVideoValidator : AbstractValidator<FaqVideoPostDTO>
    {
        public FaqVideoValidator()
        {
            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => "Bu dil üçün mətn 200 simvoldan artıq ola bilməz.");

            RuleFor(x => x.VideoUrl)
                .MaximumLength(1000).WithMessage("Video üçün url 1000 simvoldan çox olmamalıdır");

            RuleFor(x => x.PlaylistUrl)
                .MaximumLength(1000).WithMessage("Pleylist üçün url 1000 simvoldan çox olmamalıdır");
        }
    }
}
