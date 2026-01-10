using CB.Application.DTOs.Translate;
using FluentValidation;

namespace CB.Application.Validators.Translate
{
    public class TranslateCreateValidator : AbstractValidator<TranslateCreateDTO>
    {
        public TranslateCreateValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("Açar (Key) boş ola bilməz.")
                .MaximumLength(100).WithMessage("Açar 100 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Values)
                .NotNull().WithMessage("Dəyərlər göndərilməlidir.")
                .NotEmpty().WithMessage("Ən azı bir dil üçün dəyər daxil edilməlidir.");

            RuleForEach(x => x.Values)
                .Must(v => v.Value != null && v.Value.Trim().Length > 0)
                .WithMessage(v => $"Dil '{v.Key}' üçün dəyər boş ola bilməz.")
                .Must(v => v.Value == null || v.Value.Length <= 200)
                .WithMessage(v => $"Dil '{v.Key}' üçün dəyər 200 simvoldan artıq ola bilməz.");
        }
    }
}
