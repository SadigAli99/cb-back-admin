

using CB.Application.DTOs.ReceptionCitizen;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.ReceptionCitizen
{
    public class ReceptionCitizenEditValidator : AbstractValidator<ReceptionCitizenEditDTO>
    {
        public ReceptionCitizenEditValidator(
            IReceptionCitizenCategoryService statisticalReportCategoryService
        )
        {

            RuleFor(x => x.ReceptionCitizenCategoryId)
                    .MustAsync(async (statisticalReportCategoryID, cancellation) =>
                    {
                        var statisticalReportCategory = await statisticalReportCategoryService.GetByIdAsync(statisticalReportCategoryID);
                        return statisticalReportCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");

        }
    }
}
