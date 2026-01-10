

using CB.Application.DTOs.TerritorialOfficeRegion;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.TerritorialOfficeRegion
{
    public class TerritorialOfficeRegionEditValidator : AbstractValidator<TerritorialOfficeRegionEditDTO>
    {
        public TerritorialOfficeRegionEditValidator(ITerritorialOfficeService territorialOfficeService)
        {

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.Phone)
                .MaximumLength(50).WithMessage("Telefon nömrəsi 50 simvoldan çox ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Directors)
                .NotEmpty().WithMessage("Ən azı bir dil üçün rəhbər daxil edilməlidir.");

            RuleForEach(x => x.Directors)
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün rəhbər 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Locations)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ünvan daxil edilməlidir.");

            RuleForEach(x => x.Locations)
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün ünvan 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.TerritorialOfficeId)
                .NotNull().WithMessage("Ofis daxil edin")
                .MustAsync(async (territorialOfficeId, cancellation) =>
                {
                    var territorialOffice = await territorialOfficeService.GetByIdAsync(territorialOfficeId);
                    return territorialOffice != null ? true : false;
                })
                .WithMessage("Seçilən ofis yanlışdır");
        }
    }
}
