using CB.Application.DTOs.BankNote;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.BankNote
{
    public class BankNoteEditValidator : AbstractValidator<BankNoteEditDTO>
    {
        public BankNoteEditValidator(IBankNoteCategoryService percentCorridorCategoryService)
        {
            RuleFor(x => x.Date)
                .NotNull().WithMessage("Tarixi boş ola bilməz.");

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Dəyər 0-dan böyük olmalıdır");

            RuleFor(x => x.PercentValue)
                .GreaterThan(0).WithMessage("Faiz dərəcəsi 0-dan böyük olmalıdır");

            RuleFor(x => x.BankNoteCategoryId)
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
