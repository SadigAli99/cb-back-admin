using CB.Application.DTOs.Address;
using FluentValidation;

namespace CB.Application.Validators.Address
{
    public class AddressCreateValidator : AbstractValidator<AddressCreateDTO>
    {
        public AddressCreateValidator()
        {
            RuleFor(x => x.Map)
                .MaximumLength(10000).WithMessage("Xəritə linki üçün dəyər 10000 simvoldan artıq ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleFor(x => x.Texts)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ünvan daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Texts)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün ünvan boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün ünvan 500 simvoldan artıq ola bilməz.");
        }
    }
}
