
using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristicImage;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CurrencyHistoryPrevItemCharacteristicImage
{
    public class CurrencyHistoryPrevItemCharacteristicImageCreateValidator : AbstractValidator<CurrencyHistoryPrevItemCharacteristicImageCreateDTO>
    {
        public CurrencyHistoryPrevItemCharacteristicImageCreateValidator(ICurrencyHistoryPrevService currencyHistoryPrevService)
        {
            RuleFor(x => x.CurrencyHistoryPrevId)
                    .MustAsync(async (currencyHistoryPrevID, cancellation) =>
                    {
                        var currencyHistoryPrev = await currencyHistoryPrevService.GetByIdAsync(currencyHistoryPrevID);
                        return currencyHistoryPrev != null ? true : false;
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

            RuleFor(x => x.Colors)
                .NotEmpty().WithMessage("Ən azı bir dil üçün rəng daxil edilməlidir.");

            RuleForEach(x => x.Colors)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Sizes)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ölçü daxil edilməlidir.");

            RuleForEach(x => x.Sizes)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x=>x.Nominal)
                .NotEmpty().WithMessage("Nominal daxil edilməlidir")
                .MaximumLength(100).WithMessage("Nominal 100 simvoldan çox olmamalıdır.");


        }
    }
}
