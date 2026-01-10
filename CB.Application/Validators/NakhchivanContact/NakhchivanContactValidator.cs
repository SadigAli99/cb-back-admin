
using CB.Application.DTOs.NakhchivanContact;
using FluentValidation;

namespace CB.Application.Validators.NakhchivanContact
{
    public class NakhchivanContactValidator : AbstractValidator<NakhchivanContactPostDTO>
    {
        public NakhchivanContactValidator()
        {
            RuleFor(x => x.Map)
                .MaximumLength(50000).WithMessage("Xəritə 50000 simvoldan çox ola bilməz");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
