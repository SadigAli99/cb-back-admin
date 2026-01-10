using CB.Application.DTOs.Poster;
using FluentValidation;

namespace CB.Application.Validators.Poster
{
    public class PosterCreateValidator : AbstractValidator<PosterCreateDTO>
    {
        public PosterCreateValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");
        }
    }
}
