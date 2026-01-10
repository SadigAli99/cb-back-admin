using CB.Application.DTOs.MonetaryIndicator;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.MonetaryIndicator
{
    public class MonetaryIndicatorCreateValidator : AbstractValidator<MonetaryIndicatorCreateDTO>
    {
        public MonetaryIndicatorCreateValidator(IMonetaryIndicatorCategoryService percentCorridorCategoryService)
        {
            RuleFor(x => x.Date)
                .NotNull().WithMessage("Tarixi boş ola bilməz.");

            RuleFor(x => x.Value)
                .NotNull().WithMessage("Dəyəri daxil edin")
                .GreaterThan(0).WithMessage("Faiz dərəcəsi 0dan böyük olmalıdır");

            RuleFor(x => x.MonetaryIndicatorCategoryId)
                .NotNull().WithMessage("Kateqoriya daxil edin")
                .MustAsync(async (percentCorridorCategoryId, cancellation) =>
                {
                    var percentCorridorCategory = await percentCorridorCategoryService.GetByIdAsync(percentCorridorCategoryId);
                    return percentCorridorCategory != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");
        }
    }
}
