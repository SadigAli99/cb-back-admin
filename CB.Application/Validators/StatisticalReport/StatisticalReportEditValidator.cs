

using CB.Application.DTOs.StatisticalReport;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.StatisticalReport
{
    public class StatisticalReportEditValidator : AbstractValidator<StatisticalReportEditDTO>
    {
        public StatisticalReportEditValidator(
            IStatisticalReportCategoryService statisticalReportCategoryService,
            IStatisticalReportSubCategoryService statisticalReportSubCategoryService
        )
        {

            RuleFor(x => x.StatisticalReportCategoryId)
                    .MustAsync(async (statisticalReportCategoryID, cancellation) =>
                    {
                        var statisticalReportCategory = await statisticalReportCategoryService.GetByIdAsync(statisticalReportCategoryID);
                        return statisticalReportCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.StatisticalReportSubCategoryId)
                    .MustAsync(async (statisticalReportSubCategoryID, cancellation) =>
                    {
                        if (statisticalReportSubCategoryID is null) return true;
                        var statisticalReportSubCategory = await statisticalReportSubCategoryService.GetByIdAsync((int)statisticalReportSubCategoryID);
                        return statisticalReportSubCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən alt kateqoriya yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Periods)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Periods)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 1000)
                .WithMessage("Bu dil üçün mətn 1000 simvoldan artıq ola bilməz.");

        }
    }
}
