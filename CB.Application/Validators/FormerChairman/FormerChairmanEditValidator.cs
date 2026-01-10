

using CB.Application.DTOs.FormerChairman;
using FluentValidation;

namespace CB.Application.Validators.FormerChairman
{
    public class FormerChairmanEditValidator : AbstractValidator<FormerChairmanEditDTO>
    {
        public FormerChairmanEditValidator()
        {
            RuleFor(x => x.Fullnames)
                .NotEmpty().WithMessage("Ən azı bir dil üçün ad, soyad daxil edilməlidir.");

            RuleForEach(x => x.Fullnames)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün ad, soyad boş ola bilməz.")
                .Must(v => v.Value.Length <= 100)
                .WithMessage("Bu dil üçün ad, soyad 100 simvoldan artıq ola bilməz.");


            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün mətn daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün mətn boş ola bilməz.")
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
