using CB.Application.DTOs.SecurityType;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.SecurityType
{
    public class SecurityTypeEditValidator : AbstractValidator<SecurityTypeEditDTO>
    {
        public SecurityTypeEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
