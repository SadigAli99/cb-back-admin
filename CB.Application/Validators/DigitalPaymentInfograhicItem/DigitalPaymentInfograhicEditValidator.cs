

using CB.Application.DTOs.DigitalPaymentInfograhicItem;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.DigitalPaymentInfograhicItem
{
    public class DigitalPaymentInfograhicItemEditValidator : AbstractValidator<DigitalPaymentInfograhicItemEditDTO>
    {
        public DigitalPaymentInfograhicItemEditValidator(
            IDigitalPaymentInfograhicService digitalPaymentService
        )
        {

            RuleFor(x => x.DigitalPaymentInfograhicId)
                    .MustAsync(async (digitalPaymentID, cancellation) =>
                    {
                        var digitalPayment = await digitalPaymentService.GetByIdAsync(digitalPaymentID);
                        return digitalPayment != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.DigitalPaymentInfograhicId)
                    .MustAsync(async (digitalPaymentID, cancellation) =>
                    {
                        var digitalPayment = await digitalPaymentService.GetByIdAsync(digitalPaymentID);
                        return digitalPayment != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.Month)
            .NotNull().WithMessage("Ayı daxil edin")
            .GreaterThanOrEqualTo(1).WithMessage("Ayı düzgün daxil edin")
            .LessThanOrEqualTo(12).WithMessage("Ayı düzgün daxil edin");

            RuleFor(x => x.Year)
                .NotNull().WithMessage("İli daxil edin")
                .GreaterThanOrEqualTo(1990).WithMessage("İlin dəyəri 1990-dan kiçik ola bilməz");

            RuleFor(x => x.Value)
                .NotNull().WithMessage("Dəyəri daxil edin")
                .GreaterThanOrEqualTo(0).WithMessage("Dəyər 0-dan böyük olmalıdır");

        }
    }
}
