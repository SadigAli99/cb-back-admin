using CB.Application.DTOs.Social;
using FluentValidation;

namespace CB.Application.Validators.Social
{
    public class SocialEditValidator : AbstractValidator<SocialEditDTO>
    {
        public SocialEditValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
                .MaximumLength(200).WithMessage("Başlıq 200 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Url)
                .MaximumLength(500).WithMessage("URL 500 simvoldan artıq ola bilməz.");
        }
    }
}
