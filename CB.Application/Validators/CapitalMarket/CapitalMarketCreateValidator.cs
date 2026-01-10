
using CB.Application.DTOs.CapitalMarket;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CapitalMarket
{
    public class CapitalMarketCreateValidator : AbstractValidator<CapitalMarketCreateDTO>
    {
        public CapitalMarketCreateValidator(
            ICapitalMarketCategoryService creditInstitutionCategoryService
        )
        {

            RuleFor(x => x.CapitalMarketCategoryId)
                    .MustAsync(async (creditInstitutionCategoryID, cancellation) =>
                    {
                        var creditInstitutionCategory = await creditInstitutionCategoryService.GetByIdAsync(creditInstitutionCategoryID);
                        return creditInstitutionCategory != null ? true : false;
                    })
                    .WithMessage("Seçilən kateqoriya yanlışdır");

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
