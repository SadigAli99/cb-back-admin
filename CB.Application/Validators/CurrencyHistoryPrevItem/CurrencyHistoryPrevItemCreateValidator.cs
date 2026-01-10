
using CB.Application.DTOs.CurrencyHistoryPrevItem;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistoryPrevItem
{
    public class CurrencyHistoryPrevItemCreateValidator : AbstractValidator<CurrencyHistoryPrevItemCreateDTO>
    {
        public CurrencyHistoryPrevItemCreateValidator(
            ICurrencyHistoryPrevService currencyHistoryPrevService
        )
        {

            RuleFor(x => x.CurrencyHistoryPrevId)
                    .MustAsync(async (currencyHistoryPrevID, cancellation) =>
                    {
                        var currencyHistoryPrev = await currencyHistoryPrevService.GetByIdAsync(currencyHistoryPrevID);
                        return currencyHistoryPrev != null ? true : false;
                    })
                    .WithMessage("Seçilən valyuta tarixçəsi yanlışdır");

        }
    }
}
