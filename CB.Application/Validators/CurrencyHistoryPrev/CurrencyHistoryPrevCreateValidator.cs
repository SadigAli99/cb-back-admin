
using CB.Application.DTOs.CurrencyHistoryPrev;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistoryPrev
{
    public class CurrencyHistoryPrevCreateValidator : AbstractValidator<CurrencyHistoryPrevCreateDTO>
    {
        public CurrencyHistoryPrevCreateValidator(ICurrencyHistoryService currencyHistoryService)
        {
            RuleFor(x => x.CurrencyHistoryId)
                    .MustAsync(async (currencyHistoryID, cancellation) =>
                    {
                        var currencyHistory = await currencyHistoryService.GetByIdAsync(currencyHistoryID);
                        return currencyHistory != null ? true : false;
                    })
                    .WithMessage("Seçilən valyuta tarixçəsi yanlışdır");

            RuleFor(x => x.File)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");


            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün rəng daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.SubTitles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün alt başlıq daxil edilməlidir.");

            RuleForEach(x => x.SubTitles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün alt başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün alt başlıq 500 simvoldan artıq ola bilməz.");


        }
    }
}
