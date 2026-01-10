
using CB.Application.DTOs.NationalBankNoteMoneySign;
using FluentValidation;

namespace CB.Application.Validators.NationalBankNoteMoneySign
{
    public class NationalBankNoteMoneySignCreateValidator : AbstractValidator<NationalBankNoteMoneySignCreateDTO>
    {
        public NationalBankNoteMoneySignCreateValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");


        }
    }
}
