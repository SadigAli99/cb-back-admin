using CB.Application.DTOs.ClearingSettlementSystem;
using FluentValidation;

namespace CB.Application.Validators.ClearingSettlementSystem
{
    public class ClearingSettlementSystemEditValidator : AbstractValidator<ClearingSettlementSystemEditDTO>
    {
        public ClearingSettlementSystemEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => $"Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");
        }
    }
}
