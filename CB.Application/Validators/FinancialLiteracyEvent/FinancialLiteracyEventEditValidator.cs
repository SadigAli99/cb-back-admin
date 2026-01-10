
using CB.Application.DTOs.FinancialLiteracyEvent;
using FluentValidation;

namespace CB.Application.Validators.FinancialLiteracyEvent
{
    public class FinancialLiteracyEventEditValidator : AbstractValidator<FinancialLiteracyEventEditDTO>
    {
        public FinancialLiteracyEventEditValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarix boş ola bilməz");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün vəzifə daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value != null && v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
