

using CB.Application.DTOs.CurrencyHistoryPrevItem;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistoryPrevItem
{
    public class CurrencyHistoryPrevItemEditValidator : AbstractValidator<CurrencyHistoryPrevItemEditDTO>
    {
        public CurrencyHistoryPrevItemEditValidator(
            ICurrencyHistoryPrevService currencyHistoryPrevService
        )
        {

            RuleFor(x => x.CurrencyHistoryPrevId)
                    .MustAsync(async (currencyHistoryPrevID, cancellation) =>
                    {
                        var currencyHistoryPrev = await currencyHistoryPrevService.GetByIdAsync(currencyHistoryPrevID);
                        return currencyHistoryPrev != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı yanlışdır");

        }
    }
}
