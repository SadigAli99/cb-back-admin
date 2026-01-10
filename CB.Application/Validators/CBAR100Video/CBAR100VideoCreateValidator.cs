
using CB.Application.DTOs.CBAR100Video;
using FluentValidation;

namespace CB.Application.Validators.CBAR100Video
{
    public class CBAR100VideoCreateValidator : AbstractValidator<CBAR100VideoCreateDTO>
    {
        public CBAR100VideoCreateValidator()
        {
            RuleFor(x => x.Url)
                .MaximumLength(500).WithMessage("Keçid linkinin uzunluğu 500 simvoldan çox ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 2000)
                .WithMessage("Bu dil üçün başlıq 2000 simvoldan artıq ola bilməz.");
        }
    }
}
