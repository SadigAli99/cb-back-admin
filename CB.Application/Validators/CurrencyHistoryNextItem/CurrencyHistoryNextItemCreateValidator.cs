
using CB.Application.DTOs.CurrencyHistoryNextItem;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistoryNextItem
{
    public class CurrencyHistoryNextItemCreateValidator : AbstractValidator<CurrencyHistoryNextItemCreateDTO>
    {
        public CurrencyHistoryNextItemCreateValidator(ICurrencyHistoryNextService currencyHistoryNextService)
        {
            RuleFor(x => x.CurrencyHistoryNextId)
                    .MustAsync(async (currencyHistoryNextID, cancellation) =>
                    {
                        var currencyHistoryNext = await currencyHistoryNextService.GetByIdAsync(currencyHistoryNextID);
                        return currencyHistoryNext != null ? true : false;
                    })
                    .WithMessage("Seçilən valyuta tarixçəsi yanlışdır");

            RuleFor(x => x.FrontFile)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.BackFile)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");


        }
    }
}
