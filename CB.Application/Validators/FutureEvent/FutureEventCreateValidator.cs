
using CB.Application.DTOs.FutureEvent;
using FluentValidation;

namespace CB.Application.Validators.FutureEvent
{
    public class FutureEventCreateValidator : AbstractValidator<FutureEventCreateDTO>
    {
        public FutureEventCreateValidator()
        {
            RuleFor(x => x.Date)
                .NotNull().WithMessage("Tarix boş ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Locations)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ünvan daxil edilməlidir.");

            RuleForEach(x => x.Locations)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün ünvan boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün ünvan 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Formats)
                .NotEmpty().WithMessage("Ən azı bir dil üçün format daxil edilməlidir.");

            RuleForEach(x => x.Formats)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün format boş ola bilməz.")
                .Must(v => v.Value.Length <= 100)
                .WithMessage("Bu dil üçün format 100 simvoldan artıq ola bilməz.");
        }
    }
}
