
using CB.Application.DTOs.Chronology;
using FluentValidation;

namespace CB.Application.Validators.Chronology
{
    public class ChronologyCreateValidator : AbstractValidator<ChronologyCreateDTO>
    {
        public ChronologyCreateValidator()
        {
            RuleFor(x => x.Year)
                .GreaterThan(1900).WithMessage("İlin dəyəri 1900-dan böyük olmalıdır");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
