using CB.Application.DTOs.Translate;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.Translate
{
    public class TranslateEditValidator : AbstractValidator<TranslateEditDTO>
    {
        public TranslateEditValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("Açar (Key) boş ola bilməz.")
                .MaximumLength(100).WithMessage("Açar 100 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Values)
                .NotEmpty().WithMessage("Ən azı bir dil üçün dəyər daxil edilməlidir.");

            RuleForEach(x => x.Values)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => $"Dil '{v.Key}' üçün dəyər boş ola bilməz.")
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Dil '{v.Key}' üçün dəyər 200 simvoldan artıq ola bilməz.");
        }
    }
}
