

using CB.Application.DTOs.ReceptionCitizenLink;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.ReceptionCitizenLink
{
    public class ReceptionCitizenLinkEditValidator : AbstractValidator<ReceptionCitizenLinkEditDTO>
    {
        public ReceptionCitizenLinkEditValidator(IReceptionCitizenCategoryService receptionCitizenCategoryService)
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
                .MaximumLength(500).WithMessage("Keçid linki 500 simvoldan ,ox olmamalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");
        }
    }
}
