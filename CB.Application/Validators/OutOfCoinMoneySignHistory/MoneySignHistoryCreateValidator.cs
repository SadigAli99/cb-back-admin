
using CB.Application.DTOs.OutOfCoinMoneySignHistory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.OutOfCoinMoneySignHistory
{
    public class OutOfCoinMoneySignHistoryCreateValidator : AbstractValidator<OutOfCoinMoneySignHistoryCreateDTO>
    {
        public OutOfCoinMoneySignHistoryCreateValidator(
            IOutOfCoinMoneySignService outOfCoinService
        )
        {

            RuleFor(x => x.MoneySignId)
                    .MustAsync(async (outOfCoinID, cancellation) =>
                    {
                        var outOfCoin = await outOfCoinService.GetByIdAsync(outOfCoinID);
                        return outOfCoin != null ? true : false;
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
