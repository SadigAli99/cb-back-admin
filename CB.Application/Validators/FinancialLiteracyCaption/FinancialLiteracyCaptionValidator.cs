
using CB.Application.DTOs.FinancialLiteracyCaption;
using FluentValidation;

namespace CB.Application.Validators.FinancialLiteracyCaption
{
    public class FinancialLiteracyCaptionValidator : AbstractValidator<FinancialLiteracyCaptionPostDTO>
    {
        public FinancialLiteracyCaptionValidator()
        {
            RuleFor(x => x.Url)
                .MaximumLength(5000).WithMessage("Video linki 5000 simvoldan çox ola bilməz");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => "Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
