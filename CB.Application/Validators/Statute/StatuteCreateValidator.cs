
using CB.Application.DTOs.Statute;
using FluentValidation;

namespace CB.Application.Validators.Statute
{
    public class StatuteCreateValidator : AbstractValidator<StatuteCreateDTO>
    {
        public StatuteCreateValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 100000)
                .WithMessage("Faylın ölçüsü 100 MB-dan çox olmamalıdır.")
                .Must(file =>
                {
                    if (file == null) return true;

                    var allowedTypes = new[]
                    {
                        "application/pdf",
                    };

                    return allowedTypes.Contains(file.ContentType);
                })
                .WithMessage("Fayl yalnız pdf formatında ola bilər.");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.SubTitles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün alt başlıq daxil edilməlidir.");

            RuleForEach(x => x.SubTitles)
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün alt başlıq 500 simvoldan artıq ola bilməz.");


        }
    }
}
