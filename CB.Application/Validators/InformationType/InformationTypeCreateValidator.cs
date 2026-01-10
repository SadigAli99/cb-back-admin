using CB.Application.DTOs.InformationType;
using FluentValidation;

namespace CB.Application.Validators.InformationType
{
    public class InformationTypeCreateValidator : AbstractValidator<InformationTypeCreateDTO>
    {
        public InformationTypeCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");
        }
    }
}
