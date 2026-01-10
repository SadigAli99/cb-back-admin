
using System.Data;
using CB.Application.DTOs.CustomerContact;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CustomerContact
{
    public class CustomerContactCreateValidator : AbstractValidator<CustomerContactCreateDTO>
    {
        public CustomerContactCreateValidator()
        {
            RuleFor(x => x.Map)
                .MaximumLength(500).WithMessage("Xəritə linki 500 simvoldan çox olmamalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
