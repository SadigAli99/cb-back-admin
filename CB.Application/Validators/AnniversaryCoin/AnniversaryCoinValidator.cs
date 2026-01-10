
using CB.Application.DTOs.AnniversaryCoin;
using FluentValidation;

namespace CB.Application.Validators.AnniversaryCoin
{
    public class AnniversaryCoinValidator : AbstractValidator<AnniversaryCoinPostDTO>
    {
        public AnniversaryCoinValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => "Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün başlıq 50000 simvoldan artıq ola bilməz.");
        }
    }
}
