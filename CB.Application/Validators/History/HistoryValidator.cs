
using CB.Application.DTOs.History;
using FluentValidation;

namespace CB.Application.Validators.History
{
    public class HistoryValidator : AbstractValidator<HistoryPostDTO>
    {
        public HistoryValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => "Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 20000)
                .WithMessage(v => "Bu dil üçün mətn 20000 simvoldan artıq ola bilməz.");
        }
    }
}
