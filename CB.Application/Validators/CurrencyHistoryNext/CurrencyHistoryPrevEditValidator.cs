

using CB.Application.DTOs.CurrencyHistoryNext;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistoryNext
{
    public class CurrencyHistoryNextEditValidator : AbstractValidator<CurrencyHistoryNextEditDTO>
    {
        public CurrencyHistoryNextEditValidator(ICurrencyHistoryService currencyHistoryService)
        {
            RuleFor(x => x.CurrencyHistoryId)
                    .MustAsync(async (currencyHistoryID, cancellation) =>
                    {
                        var currencyHistory = await currencyHistoryService.GetByIdAsync(currencyHistoryID);
                        return currencyHistory != null ? true : false;
                    })
                    .WithMessage("Seçilən valyuta tarixçəsi yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün rəng daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün alt başlıq daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
