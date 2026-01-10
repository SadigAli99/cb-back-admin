

using CB.Application.DTOs.CoinMoneySignHistory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CoinMoneySignHistory
{
    public class CoinMoneySignHistoryEditValidator : AbstractValidator<CoinMoneySignHistoryEditDTO>
    {
        public CoinMoneySignHistoryEditValidator(
            ICoinMoneySignService coinMoneySignService
        )
        {

            RuleFor(x => x.MoneySignId)
                    .MustAsync(async (coinMoneySignID, cancellation) =>
                    {
                        var coinMoneySign = await coinMoneySignService.GetByIdAsync(coinMoneySignID);
                        return coinMoneySign != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı yanlışdır");


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
