
using CB.Application.DTOs.OtherInfo;
using FluentValidation;

namespace CB.Application.Validators.OtherInfo
{
    public class OtherInfoCreateValidator : AbstractValidator<OtherInfoCreateDTO>
    {
        public OtherInfoCreateValidator()
        {
            RuleFor(x => x.Url)
                .MaximumLength(500).WithMessage("Keçid linkinin uzunluğu 500 simvoldan çox ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");
        }
    }
}
