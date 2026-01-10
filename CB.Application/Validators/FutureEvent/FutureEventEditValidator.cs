

using CB.Application.DTOs.FutureEvent;
using FluentValidation;

namespace CB.Application.Validators.FutureEvent
{
    public class FutureEventEditValidator : AbstractValidator<FutureEventEditDTO>
    {
        public FutureEventEditValidator()
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
        }
    }
}
