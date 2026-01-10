
using CB.Application.DTOs.Volunteer;
using FluentValidation;

namespace CB.Application.Validators.Volunteer
{
    public class VolunteerCreateValidator : AbstractValidator<VolunteerCreateDTO>
    {
        public VolunteerCreateValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.Fullnames)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ad,soyad daxil edilməlidir.");

            RuleForEach(x => x.Fullnames)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün ad,soyad boş ola bilməz.")
                .Must(v => v.Value.Length <= 100)
                .WithMessage("Bu dil üçün ad,soyad 100 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün vəzifə daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün vəzifə boş ola bilməz.")
                .Must(v => v.Value.Length <= 20000)
                .WithMessage("Bu dil üçün vəzifə 20000 simvoldan artıq ola bilməz.");
        }
    }
}
