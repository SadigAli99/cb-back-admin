using CB.Application.DTOs.InsuranceStatisticSubCategory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.InsuranceStatisticSubCategory
{
    public class InsuranceStatisticSubCategoryCreateValidator : AbstractValidator<InsuranceStatisticSubCategoryCreateDTO>
    {
        public InsuranceStatisticSubCategoryCreateValidator(IInsuranceStatisticCategoryService insuranceStatisticCategoryService)
        {

            RuleFor(x => x.InsuranceStatisticCategoryId)
                    .NotNull().WithMessage("Kateqoriya seçin")
                    .MustAsync(async (insuranceStatisticCategoryID, cancellation) =>
                    {
                        var insuranceStatisticCategory = await insuranceStatisticCategoryService.GetByIdAsync(insuranceStatisticCategoryID);
                        return insuranceStatisticCategory != null ? true : false;
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
