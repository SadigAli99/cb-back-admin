using CB.Application.DTOs.RealTimeSettlementSystem;
using FluentValidation;

namespace CB.Application.Validators.RealTimeSettlementSystem
{
    public class RealTimeSettlementSystemEditValidator : AbstractValidator<RealTimeSettlementSystemEditDTO>
    {
        public RealTimeSettlementSystemEditValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 500)
                .WithMessage(v => $"Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");
        }
    }
}
