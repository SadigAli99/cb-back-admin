
using CB.Application.DTOs.PaperMoney;
using FluentValidation;

namespace CB.Application.Validators.PaperMoney
{
    public class PaperMoneyCreateValidator : AbstractValidator<PaperMoneyCreateDTO>
    {
        public PaperMoneyCreateValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 255)
                .WithMessage("Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");


            RuleForEach(x => x.Topics)
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün mövzu 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.ReleaseDates)
                .Must(v => v.Value == null || v.Value.Length <= 50)
                .WithMessage("Bu dil üçün buraxılış tarixi 50 simvoldan artıq ola bilməz.");


        }
    }
}
