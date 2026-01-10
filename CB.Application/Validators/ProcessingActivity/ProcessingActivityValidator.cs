
using CB.Application.DTOs.ProcessingActivity;
using FluentValidation;

namespace CB.Application.Validators.ProcessingActivity
{
    public class ProcessingActivityValidator : AbstractValidator<ProcessingActivityPostDTO>
    {
        public ProcessingActivityValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
