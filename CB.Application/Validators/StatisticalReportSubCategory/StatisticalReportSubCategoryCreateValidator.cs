using CB.Application.DTOs.StatisticalReportSubCategory;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.StatisticalReportSubCategory
{
    public class StatisticalReportSubCategoryCreateValidator : AbstractValidator<StatisticalReportSubCategoryCreateDTO>
    {
        public StatisticalReportSubCategoryCreateValidator(IStatisticalReportCategoryService statisticalReportCategoryService)
        {

            RuleFor(x => x.StatisticalReportCategoryId)
                    .NotNull().WithMessage("Kateqoriya seçin")
                    .MustAsync(async (statisticalReportCategoryID, cancellation) =>
                    {
                        var statisticalReportCategory = await statisticalReportCategoryService.GetByIdAsync(statisticalReportCategoryID);
                        return statisticalReportCategory != null ? true : false;
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
