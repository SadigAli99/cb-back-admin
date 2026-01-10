
using CB.Application.DTOs.MoneySignProtection;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.MoneySignProtection
{
    public class MoneySignProtectionCreateValidator : AbstractValidator<MoneySignProtectionCreateDTO>
    {
        public MoneySignProtectionCreateValidator(IMoneySignHistoryService moneySignHistoryService)
        {
            RuleFor(x => x.MoneySignHistoryId)
                    .MustAsync(async (moneySignHistoryID, cancellation) =>
                    {
                        var moneySignHistory = await moneySignHistoryService.GetByIdAsync(moneySignHistoryID);
                        return moneySignHistory != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı tarixçəsi yanlışdır");

            RuleFor(x => x.File)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");


        }
    }
}
