
using CB.Application.DTOs.MoneySignCharacteristicImage;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.MoneySignCharacteristicImage
{
    public class MoneySignCharacteristicImageCreateValidator : AbstractValidator<MoneySignCharacteristicImageCreateDTO>
    {
        public MoneySignCharacteristicImageCreateValidator(IMoneySignHistoryService moneySignHistoryService)
        {
            RuleFor(x => x.MoneySignHistoryId)
                    .MustAsync(async (moneySignHistoryID, cancellation) =>
                    {
                        var moneySignHistory = await moneySignHistoryService.GetByIdAsync(moneySignHistoryID);
                        return moneySignHistory != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı tarixçəsi yanlışdır");

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
