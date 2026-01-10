using CB.Application.DTOs.IssuerType;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.IssuerType
{
    public class IssuerTypeEditValidator : AbstractValidator<IssuerTypeEditDTO>
    {
        public IssuerTypeEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
