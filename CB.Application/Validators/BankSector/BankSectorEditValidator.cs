using CB.Application.DTOs.BankSector;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.BankSector
{
    public class BankSectorEditValidator : AbstractValidator<BankSectorEditDTO>
    {
        public BankSectorEditValidator(IBankSectorCategoryService percentCorridorCategoryService)
        {
            RuleFor(x => x.Date)
                .NotNull().WithMessage("Tarixi boş ola bilməz.");

            RuleFor(x => x.Value)
                .NotNull().WithMessage("Dəyəri daxil edin")
                .GreaterThan(0).WithMessage("Faiz dərəcəsi 0dan böyük olmalıdır");

            RuleFor(x => x.BankSectorCategoryId)
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
