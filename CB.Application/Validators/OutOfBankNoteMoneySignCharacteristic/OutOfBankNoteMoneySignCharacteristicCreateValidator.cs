
using CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristic;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.OutOfBankNoteMoneySignCharacteristic
{
    public class OutOfBankNoteMoneySignCharacteristicCreateValidator : AbstractValidator<OutOfBankNoteMoneySignCharacteristicCreateDTO>
    {
        public OutOfBankNoteMoneySignCharacteristicCreateValidator(
            IMoneySignHistoryService moneySignHistoryService
        )
        {

            RuleFor(x => x.MoneySignHistoryId)
                    .MustAsync(async (moneySignHistoryID, cancellation) =>
                    {
                        var moneySignHistory = await moneySignHistoryService.GetByIdAsync(moneySignHistoryID);
                        return moneySignHistory != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı tarixçəsi yanlışdır");


            RuleFor(x => x.Labels)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Labels)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Values)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Values)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 1000)
                .WithMessage("Bu dil üçün mətn 1000 simvoldan artıq ola bilməz.");
        }
    }
}
