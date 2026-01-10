

using CB.Application.DTOs.OutOfCirculationCategory;
using FluentValidation;

namespace CB.Application.Validators.OutOfCirculationCategory
{
    public class OutOfCirculationCategoryEditValidator : AbstractValidator<OutOfCirculationCategoryEditDTO>
    {
        public OutOfCirculationCategoryEditValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 255)
                .WithMessage("Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");

            RuleFor(x=>x.Type)
                .NotNull().WithMessage("Əskinas növünü seçin")
                .IsInEnum().WithMessage("Seçilən əskinas növü yanlışdır");
        }
    }
}
