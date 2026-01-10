

using CB.Application.DTOs.Director;
using FluentValidation;

namespace CB.Application.Validators.Director
{
    public class DirectorEditValidator : AbstractValidator<DirectorEditDTO>
    {
        public DirectorEditValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.Type)
                .NotNull().WithMessage("Rəhbər növünü seçin.")
                .IsInEnum().WithMessage("Rəhbər növü yanlışdır");

            RuleFor(x => x.Fullnames)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ad,soyad daxil edilməlidir.");

            RuleForEach(x => x.Fullnames)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün ad,soyad boş ola bilməz.")
                .Must(v => v.Value.Length <= 100)
                .WithMessage("Bu dil üçün ad,soyad 100 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Positions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün vəzifə daxil edilməlidir.");

            RuleForEach(x => x.Positions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün vəzifə boş ola bilməz.")
                .Must(v => v.Value.Length <= 200)
                .WithMessage("Bu dil üçün vəzifə 200 simvoldan artıq ola bilməz.");
        }
    }
}
