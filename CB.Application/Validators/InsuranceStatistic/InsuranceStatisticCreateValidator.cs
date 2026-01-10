
using CB.Application.DTOs.InsuranceStatistic;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.InsuranceStatistic
{
    public class InsuranceStatisticCreateValidator : AbstractValidator<InsuranceStatisticCreateDTO>
    {
        public InsuranceStatisticCreateValidator(
            IInsuranceStatisticCategoryService insuranceStatisticCategoryService,
            IInsuranceStatisticSubCategoryService insuranceStatisticSubCategoryService
        )
        {

            RuleFor(x => x.InsuranceStatisticCategoryId)
                    .MustAsync(async (insuranceStatisticCategoryID, cancellation) =>
                    {
                        var insuranceStatisticCategory = await insuranceStatisticCategoryService.GetByIdAsync(insuranceStatisticCategoryID);
                        return insuranceStatisticCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.InsuranceStatisticSubCategoryId)
                    .MustAsync(async (insuranceStatisticSubCategoryID, cancellation) =>
                    {
                        if (insuranceStatisticSubCategoryID is null) return true;

                        var insuranceStatisticSubCategory = await insuranceStatisticSubCategoryService.GetByIdAsync((int)insuranceStatisticSubCategoryID);
                        return insuranceStatisticSubCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən alt kateqoriya yanlışdır");

            RuleFor(x => x.File)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 100000)
                .WithMessage("Faylın ölçüsü 100 MB-dan çox olmamalıdır.")
                .Must(file =>
                {
                    if (file == null) return true;

                    var allowedTypes = new[]
                    {
                        "application/pdf",
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "application/vnd.ms-excel",
                        "application/msword",
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    };

                    return allowedTypes.Contains(file.ContentType);
                })
                .WithMessage("Fayl yalnız pdf, xlsx və ya doc formatında ola bilər.");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.CoverTitles)
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün fayl başlığı 500 simvoldan artıq ola bilməz.");


        }
    }
}
