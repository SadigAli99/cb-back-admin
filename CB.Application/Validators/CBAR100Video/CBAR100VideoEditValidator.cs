

using CB.Application.DTOs.CBAR100Video;
using FluentValidation;

namespace CB.Application.Validators.CBAR100Video
{
    public class CBAR100VideoEditValidator : AbstractValidator<CBAR100VideoEditDTO>
    {
        public CBAR100VideoEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Url)
                .MaximumLength(2000).WithMessage("Keçid linkinin uzunluğu 2000 simvoldan çox ola bilməz");
        }
    }
}
