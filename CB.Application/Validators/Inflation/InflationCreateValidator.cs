using CB.Application.DTOs.Inflation;
using FluentValidation;

namespace CB.Application.Validators.Inflation
{
    public class InflationCreateValidator : AbstractValidator<InflationCreateDTO>
    {
        public InflationCreateValidator()
        {
            RuleFor(x => x.Month)
                .NotNull().WithMessage("Ayı daxil edin")
                .GreaterThanOrEqualTo(1).WithMessage("Ayı düzgün daxil edin")
                .LessThanOrEqualTo(12).WithMessage("Ayı düzgün daxil edin");

            RuleFor(x => x.Year)
                .NotNull().WithMessage("İli daxil edin")
                .GreaterThanOrEqualTo(1990).WithMessage("İlin dəyəri 1990-dan kiçik ola bilməz");

            RuleFor(x => x.Value)
                .NotNull().WithMessage("Dəyəri daxil edin")
                .GreaterThan(0).WithMessage("Dəyər 0-dan böyük olmalıdır");
        }
    }
}
