
using CB.Application.DTOs.CBAR105Event;
using FluentValidation;

namespace CB.Application.Validators.CBAR105Event
{
    public class CBAR105EventValidator : AbstractValidator<CBAR105EventPostDTO>
    {
        public CBAR105EventValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Files)
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");
        }
    }
}
