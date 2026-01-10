

using CB.Application.DTOs.CurrencyHistoryNextItem;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistoryNextItem
{
    public class CurrencyHistoryNextItemEditValidator : AbstractValidator<CurrencyHistoryNextItemEditDTO>
    {
        public CurrencyHistoryNextItemEditValidator(ICurrencyHistoryNextService CurrencyHistoryNextService)
        {
            RuleFor(x => x.CurrencyHistoryNextId)
                    .MustAsync(async (CurrencyHistoryNextID, cancellation) =>
                    {
                        var CurrencyHistoryNext = await CurrencyHistoryNextService.GetByIdAsync(CurrencyHistoryNextID);
                        return CurrencyHistoryNext != null ? true : false;
                    })
                    .WithMessage("Seçilən valyuta tarixçəsi yanlışdır");

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
