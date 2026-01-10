using CB.Application.DTOs.CreditInstitutionSubCategory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CreditInstitutionSubCategory
{
    public class CreditInstitutionSubCategoryCreateValidator : AbstractValidator<CreditInstitutionSubCategoryCreateDTO>
    {
        public CreditInstitutionSubCategoryCreateValidator(ICreditInstitutionCategoryService creditInstitutionCategoryService)
        {

            RuleFor(x => x.CreditInstitutionCategoryId)
                    .NotNull().WithMessage("Kateqoriya seçin")
                    .MustAsync(async (creditInstitutionCategoryID, cancellation) =>
                    {
                        var creditInstitutionCategory = await creditInstitutionCategoryService.GetByIdAsync(creditInstitutionCategoryID);
                        return creditInstitutionCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
