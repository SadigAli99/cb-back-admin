

using CB.Application.DTOs.CoinMoneySignCharacteristicImage;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CoinMoneySignCharacteristicImage
{
    public class CoinMoneySignCharacteristicImageEditValidator : AbstractValidator<CoinMoneySignCharacteristicImageEditDTO>
    {
        public CoinMoneySignCharacteristicImageEditValidator(ICoinMoneySignHistoryService moneySignHistoryService)
        {
            RuleFor(x => x.MoneySignHistoryId)
                    .MustAsync(async (moneySignHistoryID, cancellation) =>
                    {
                        var moneySignHistory = await moneySignHistoryService.GetByIdAsync(moneySignHistoryID);
                        return moneySignHistory != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı tarixçəsi yanlışdır");

            RuleFor(x => x.FrontFile)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");

            RuleFor(x => x.BackFile)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");
        }
    }
}
