using CB.Application.DTOs.PercentCorridor;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.PercentCorridor
{
    public class PercentCorridorCreateValidator : AbstractValidator<PercentCorridorCreateDTO>
    {
        public PercentCorridorCreateValidator(IPercentCorridorCategoryService percentCorridorCategoryService)
        {
            RuleFor(x => x.Date)
                .NotNull().WithMessage("Tarixi boş ola bilməz.");

            RuleFor(x => x.Value)
                .NotNull().WithMessage("Dəyəri daxil edin")
                .GreaterThan(0).WithMessage("Faiz dərəcəsi 0dan böyük olmalıdır");

            RuleFor(x => x.PercentCorridorCategoryId)
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
