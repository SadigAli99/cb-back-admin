

using CB.Application.DTOs.CentralBankCooperation;
using FluentValidation;

namespace CB.Application.Validators.CentralBankCooperation
{
    public class CentralBankCooperationEditValidator : AbstractValidator<CentralBankCooperationEditDTO>
    {
        public CentralBankCooperationEditValidator()
        {
            RuleFor(x => x.Month)
                .NotNull().WithMessage("Ayı daxil edin")
                .GreaterThanOrEqualTo(1).WithMessage("Ayı düzgün daxil edin")
                .LessThanOrEqualTo(12).WithMessage("Ayı düzgün daxil edin");

            RuleFor(x => x.Year)
                .NotNull().WithMessage("İli daxil edin")
                .GreaterThanOrEqualTo(1990).WithMessage("İlin dəyəri 1990-dan kiçik ola bilməz");

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
