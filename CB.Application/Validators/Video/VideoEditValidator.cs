

using CB.Application.DTOs.Video;
using FluentValidation;

namespace CB.Application.Validators.Video
{
    public class VideoEditValidator : AbstractValidator<VideoEditDTO>
    {
        public VideoEditValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarix boş ola bilməz.")
                .Must(d => d != default).WithMessage("Tarix düzgün formatda olmalıdır (yyyy-MM-dd).");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Url)
                .MaximumLength(500).WithMessage("Keçid linkinin uzunluğu 500 simvoldan çox ola bilməz");
        }
    }
}
