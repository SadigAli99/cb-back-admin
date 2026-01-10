using CB.Application.DTOs.CitizenApplication;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CitizenApplication
{
    public class CitizenApplicationCreateValidator : AbstractValidator<CitizenApplicationCreateDTO>
    {
        public CitizenApplicationCreateValidator(ICitizenApplicationCategoryService citizenApplicationCategoryService)
        {
            RuleFor(x => x.Month)
                .NotNull().WithMessage("Ayı daxil edin")
                .GreaterThanOrEqualTo(1).WithMessage("Ayı düzgün daxil edin")
                .LessThanOrEqualTo(12).WithMessage("Ayı düzgün daxil edin");

            RuleFor(x => x.Year)
                .NotNull().WithMessage("İli daxil edin")
                .GreaterThanOrEqualTo(1990).WithMessage("İlin dəyəri 1990-dan kiçik ola bilməz");

            RuleFor(x => x.CreditInstitutionCount)
                .NotNull().WithMessage("Kredit təşkilat dəyərini daxil edin")
                .GreaterThanOrEqualTo(0).WithMessage("Kredit təşkilat dəyəri 0dan böyük olmalıdır");

            RuleFor(x => x.PaymentSystemCount)
                .NotNull().WithMessage("Ödəniş sistemi dəyərini daxil edin")
                .GreaterThanOrEqualTo(0).WithMessage("Ödəniş sistemi dəyəri 0dan böyük olmalıdır");

            RuleFor(x => x.CurrencyExchangeCount)
                .NotNull().WithMessage("Valyuta mübadiləsi məntəqəsi dəyərini daxil edin")
                .GreaterThanOrEqualTo(0).WithMessage("Valyuta mübadiləsi məntəqəsi dəyəri 0dan böyük olmalıdır");

            RuleFor(x => x.InsurerCount)
                .NotNull().WithMessage("Sığortaçı sayı dəyərini daxil edin")
                .GreaterThanOrEqualTo(0).WithMessage("Sığortaçı sayı dəyəri 0dan böyük olmalıdır");

            RuleFor(x => x.CapitalMarketCount)
                .NotNull().WithMessage("Kapital bazar dəyərini daxil edin")
                .GreaterThanOrEqualTo(0).WithMessage("Kapital bazar dəyəri 0dan böyük olmalıdır");

            RuleFor(x => x.OtherCount)
                .NotNull().WithMessage("Digər dəyərini daxil edin")
                .GreaterThanOrEqualTo(0).WithMessage("Digər dəyəri 0dan böyük olmalıdır");

            RuleFor(x => x.CitizenApplicationCategoryId)
                .NotNull().WithMessage("Kateqoriya daxil edin")
                .MustAsync(async (citizenApplicationCategoryId, cancellation) =>
                {
                    var citizenApplicationCategory = await citizenApplicationCategoryService.GetByIdAsync(citizenApplicationCategoryId);
                    return citizenApplicationCategory != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");
        }
    }
}
