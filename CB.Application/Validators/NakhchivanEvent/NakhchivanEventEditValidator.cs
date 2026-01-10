

using CB.Application.DTOs.NakhchivanEvent;
using FluentValidation;

namespace CB.Application.Validators.NakhchivanEvent
{
    public class NakhchivanEventEditValidator : AbstractValidator<NakhchivanEventEditDTO>
    {
        public NakhchivanEventEditValidator()
        {

            RuleForEach(x => x.Files)
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 20000)
                .WithMessage("Bu dil üçün mətn atributu 20000 simvoldan artıq ola bilməz.");
        }
    }
}
