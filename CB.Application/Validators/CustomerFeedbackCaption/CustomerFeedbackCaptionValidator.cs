
using CB.Application.DTOs.CustomerFeedbackCaption;
using FluentValidation;

namespace CB.Application.Validators.CustomerFeedbackCaption
{
    public class CustomerFeedbackCaptionValidator : AbstractValidator<CustomerFeedbackCaptionPostDTO>
    {
        public CustomerFeedbackCaptionValidator()
        {
            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
