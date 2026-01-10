
using CB.Application.DTOs.EventImage;
using FluentValidation;

namespace CB.Application.Validators.EventImage
{
    public class EventImageCreateValidator : AbstractValidator<EventImageCreateDTO>
    {
        public EventImageCreateValidator()
        {

            RuleForEach(x => x.ImageFiles)
                .NotNull().WithMessage("Ən azı bir şəkil seçin")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");
        }
    }
}
