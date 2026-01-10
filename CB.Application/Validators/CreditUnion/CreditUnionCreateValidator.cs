
using CB.Application.DTOs.CreditUnion;
using FluentValidation;

namespace CB.Application.Validators.CreditUnion
{
    public class CreditUnionCreateValidator : AbstractValidator<CreditUnionCreateDTO>
    {
        public CreditUnionCreateValidator()
        {

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
