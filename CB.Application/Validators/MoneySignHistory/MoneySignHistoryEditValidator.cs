

using CB.Application.DTOs.MoneySignHistory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.MoneySignHistory
{
    public class MoneySignHistoryEditValidator : AbstractValidator<MoneySignHistoryEditDTO>
    {
        public MoneySignHistoryEditValidator(
            INationalBankNoteMoneySignService nationalBankNoteService
        )
        {

            RuleFor(x => x.MoneySignId)
                    .MustAsync(async (nationalBankNoteID, cancellation) =>
                    {
                        var nationalBankNote = await nationalBankNoteService.GetByIdAsync(nationalBankNoteID);
                        return nationalBankNote != null ? true : false;
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
