

using CB.Application.DTOs.MoneySignProtection;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.MoneySignProtection
{
    public class MoneySignProtectionEditValidator : AbstractValidator<MoneySignProtectionEditDTO>
    {
        public MoneySignProtectionEditValidator(IMoneySignHistoryService moneySignHistoryService)
        {
            RuleFor(x => x.MoneySignHistoryId)
                    .MustAsync(async (moneySignHistoryID, cancellation) =>
                    {
                        var moneySignHistory = await moneySignHistoryService.GetByIdAsync(moneySignHistoryID);
                        return moneySignHistory != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı tarixçəsi yanlışdır");

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");
        }
    }
}
