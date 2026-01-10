
using CB.Application.DTOs.LotteryVideo;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.LotteryVideo
{
    public class LotteryVideoCreateValidator : AbstractValidator<LotteryVideoCreateDTO>
    {
        public LotteryVideoCreateValidator(ILotteryService lotteryService)
        {
            RuleFor(x => x.Url)
                .NotNull().WithMessage("Video linkini daxil edin")
                .MaximumLength(1000).WithMessage("Video linki 1000 simvoldan çox ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.LotteryId)
                .NotNull().WithMessage("Kateqoriya daxil edin")
                .MustAsync(async (lotteryId, cancellation) =>
                {
                    var lottery = await lotteryService.GetByIdAsync(lotteryId);
                    return lottery != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");


        }
    }
}
