
using CB.Application.DTOs.MediaQuery;
using FluentValidation;

namespace CB.Application.Validators.MediaQuery
{
    public class MediaQueryValidator : AbstractValidator<MediaQueryPostDTO>
    {
        public MediaQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Emaili daxil edin")
                .MaximumLength(100).WithMessage("Emailin uzunluğu 100 simvoldan çox ola bilməz");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon nömrəsini daxil edin")
                .MaximumLength(100).WithMessage("Telefon nömrəsinin uzunluğu 100 simvoldan çox ola bilməz");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
