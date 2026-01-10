

using CB.Application.DTOs.CBAR100Gallery;
using FluentValidation;

namespace CB.Application.Validators.CBAR100Gallery
{
    public class CBAR100GalleryEditValidator : AbstractValidator<CBAR100GalleryEditDTO>
    {
        public CBAR100GalleryEditValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");
        }
    }
}
