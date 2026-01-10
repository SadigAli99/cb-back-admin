

using CB.Application.DTOs.ReceptionCitizenVideo;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.ReceptionCitizenVideo
{
    public class ReceptionCitizenVideoEditValidator : AbstractValidator<ReceptionCitizenVideoEditDTO>
    {
        public ReceptionCitizenVideoEditValidator(IReceptionCitizenCategoryService receptionCitizenCategoryService)
        {
            RuleFor(x => x.ReceptionCitizenCategoryId)
                .MustAsync(async (ReceptionCitizenCategoryID, cancellation) =>
                {
                    var statisticalReportCategory = await receptionCitizenCategoryService.GetByIdAsync(ReceptionCitizenCategoryID);
                    return statisticalReportCategory != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Keçid linki tələb olunur")
                .MaximumLength(500).WithMessage("Keçid linki 500 simvoldan çox olmamalıdır");
        }
    }
}
