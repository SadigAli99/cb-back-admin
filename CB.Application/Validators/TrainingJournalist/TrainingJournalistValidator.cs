
using CB.Application.DTOs.TrainingJournalist;
using FluentValidation;

namespace CB.Application.Validators.TrainingJournalist
{
    public class TrainingJournalistValidator : AbstractValidator<TrainingJournalistPostDTO>
    {
        public TrainingJournalistValidator()
        {
            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => "Bu dil üçün mətn 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Files)
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");
        }
    }
}
