
using CB.Application.DTOs.CBAR100Gallery;
using FluentValidation;

namespace CB.Application.Validators.CBAR100Gallery
{
    public class CBAR100GalleryCreateValidator : AbstractValidator<CBAR100GalleryCreateDTO>
    {
        public CBAR100GalleryCreateValidator()
        {


            RuleFor(x => x.File)
                .NotNull().WithMessage("Zəhmət olmasa, faylı seçin")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");
        }
    }
}
