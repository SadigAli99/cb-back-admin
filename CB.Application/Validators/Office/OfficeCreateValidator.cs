
using CB.Application.DTOs.Office;
using FluentValidation;

namespace CB.Application.Validators.Office
{
    public class OfficeCreateValidator : AbstractValidator<OfficeCreateDTO>
    {
        public OfficeCreateValidator()
        {
            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("Zəhmət olmasa, faylı seçin")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.StatuteFile)
                .NotNull().WithMessage("Zəhmət olmasa, faylı seçin")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("application/pdf"))
                .WithMessage("Fayl PDF formatında olmalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 100)
                .WithMessage("Bu dil üçün başlıq 100 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Chairmen)
                .NotEmpty().WithMessage("Ən azı bir dil üçün rəhbər daxil edilməlidir.");

            RuleForEach(x => x.Chairmen)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün rəhbər boş ola bilməz.")
                .Must(v => v.Value.Length <= 100)
                .WithMessage("Bu dil üçün rəhbər 100 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Addresses)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ünvan daxil edilməlidir.");

            RuleForEach(x => x.Addresses)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün üvnan boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün üvnan 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Phone)
                .MaximumLength(100).WithMessage("Telefon nömrəsinin uzunluğu 100 simvoldan çox ola bilməz");
        }
    }
}
