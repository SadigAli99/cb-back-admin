using CB.Application.DTOs.ReceptionCitizenCategory;
using FluentValidation;

namespace CB.Application.Validators.ReceptionCitizenCategory
{
    public class ReceptionCitizenCategoryCreateValidator : AbstractValidator<ReceptionCitizenCategoryCreateDTO>
    {
        public ReceptionCitizenCategoryCreateValidator()
        {

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 200)
                .WithMessage(v => $"Bu dil üçün başlıq 200 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage(v => $"Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
