
using CB.Application.DTOs.CoinMoneySignHistoryFeature;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CoinMoneySignHistoryFeature
{
    public class CoinMoneySignHistoryFeatureValidator : AbstractValidator<CoinMoneySignHistoryFeaturePostDTO>
    {
        public CoinMoneySignHistoryFeatureValidator(ICoinMoneySignHistoryService moneySignHistoryService)
        {
            RuleFor(x => x.MoneySignHistoryId)
                .NotEmpty().WithMessage("Pul nişanı tarixçəsini seçin")
                .MustAsync(async (moneySignHistoryId, cancellationToken) =>
                {
                    var moneySignHistory = await moneySignHistoryService.GetByIdAsync(moneySignHistoryId);
                    return moneySignHistory != null ? true : false;
                }).WithMessage("Seçilən pul nişanı tarixçəsi yanlışdır");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
