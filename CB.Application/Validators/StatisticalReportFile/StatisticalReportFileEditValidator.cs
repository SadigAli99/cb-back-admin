

using CB.Application.DTOs.StatisticalReportFile;
using FluentValidation;

namespace CB.Application.Validators.StatisticalReportFile
{
    public class StatisticalReportFileEditValidator : AbstractValidator<StatisticalReportFileEditDTO>
    {
        public StatisticalReportFileEditValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("application/pdf"))
                .WithMessage("Fayl pdf formatında olmalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.CoverTitles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün fayl başlığı daxil edilməlidir.");

            RuleForEach(x => x.CoverTitles)
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün fayl başlığı 500 simvoldan artıq ola bilməz.");
        }
    }
}
