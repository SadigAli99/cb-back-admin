using CB.Application.DTOs.Phone;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.Phone
{
    public class PhoneEditValidator : AbstractValidator<PhoneEditDTO>
    {
        public PhoneEditValidator()
        {
            RuleFor(x => x.ContactNumber)
                .MaximumLength(100).WithMessage("Telefon nömrəsi 100 simvoldan artıq ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => "Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
