
using CB.Application.DTOs.OutOfBankNoteMoneySignHistory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.OutOfBankNoteMoneySignHistory
{
    public class OutOfBankNoteMoneySignHistoryCreateValidator : AbstractValidator<OutOfBankNoteMoneySignHistoryCreateDTO>
    {
        public OutOfBankNoteMoneySignHistoryCreateValidator(
            IOutOfBankNoteMoneySignService outOfBankNoteService
        )
        {

            RuleFor(x => x.MoneySignId)
                    .MustAsync(async (outOfBankNoteID, cancellation) =>
                    {
                        var outOfBankNote = await outOfBankNoteService.GetByIdAsync(outOfBankNoteID);
                        return outOfBankNote != null ? true : false;
                    })
                    .WithMessage("Seçilən pul nişanı yanlışdır");


            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

        }
    }
}
