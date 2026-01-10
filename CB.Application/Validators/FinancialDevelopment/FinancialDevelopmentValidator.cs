
using CB.Application.DTOs.FinancialDevelopment;
using FluentValidation;

namespace CB.Application.Validators.FinancialDevelopment
{
    public class FinancialDevelopmentValidator : AbstractValidator<FinancialDevelopmentPostDTO>
    {
        public FinancialDevelopmentValidator()
        {

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır")
                .Must(file => file == null || file.ContentType.Contains("application/pdf"))
                .WithMessage("Fayl pdf formatında olmalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => "Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");

            RuleFor(x => x.FileHeadTitles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün fayl başlığı daxil edilməlidir.");

            RuleForEach(x => x.FileHeadTitles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün fayl başlığı boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => "Bu dil üçün fayl başlığı 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.FileTitles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün fayl başlığı daxil edilməlidir.");

            RuleForEach(x => x.FileTitles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün fayl başlığı boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => "Bu dil üçün fayl başlığı 500 simvoldan artıq ola bilməz.");
        }
    }
}
