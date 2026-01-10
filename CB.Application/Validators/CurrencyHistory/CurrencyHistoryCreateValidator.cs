using CB.Application.DTOs.CurrencyHistory;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistory
{
    public class CurrencyHistoryCreateValidator : AbstractValidator<CurrencyHistoryCreateDTO>
    {
        public CurrencyHistoryCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
