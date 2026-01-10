
using CB.Application.DTOs.MonetaryPolicyVideo;
using FluentValidation;

namespace CB.Application.Validators.MonetaryPolicyVideo
{
    public class MonetaryPolicyVideoValidator : AbstractValidator<MonetaryPolicyVideoPostDTO>
    {
        public MonetaryPolicyVideoValidator()
        {
            RuleFor(x => x.Url)
                .MaximumLength(10000)
                .WithMessage(v => "Bu dil üçün link 10000 simvoldan artıq ola bilməz.");
        }
    }
}
