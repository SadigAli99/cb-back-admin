using CB.Application.DTOs.IssuerType;
using FluentValidation;

namespace CB.Application.Validators.IssuerType
{
    public class IssuerTypeCreateValidator : AbstractValidator<IssuerTypeCreateDTO>
    {
        public IssuerTypeCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
