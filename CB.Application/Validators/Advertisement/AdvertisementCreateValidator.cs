
using CB.Application.DTOs.Advertisement;
using FluentValidation;

namespace CB.Application.Validators.Advertisement
{
    public class AdvertisementCreateValidator : AbstractValidator<AdvertisementCreateDTO>
    {
        public AdvertisementCreateValidator()
        {
            RuleFor(x => x.Date)
                .NotNull().WithMessage("Tarix boş ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaTitles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün meta başlıq 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaDescriptions)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün meta təsvir 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaKeywords)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün meta keyword 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.ShortDescriptions)
                .Must(v => v.Value.Length <= 1000)
                .WithMessage("Bu dil üçün qısa mətn 1000 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
