

using CB.Application.DTOs.DirectorDetail;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.DirectorDetail
{
    public class DirectorDetailEditValidator : AbstractValidator<DirectorDetailEditDTO>
    {
        public DirectorDetailEditValidator(IDirectorService directorService)
        {
            RuleFor(x => x.DirectorId)
                    .NotNull().WithMessage("Rəhbər şəxsi seçin")
                    .MustAsync(async (directorId, cancellation) =>
                    {
                        var director = await directorService.GetByIdAsync(directorId);
                        return director != null ? true : false;
                    })
                    .WithMessage("Seçilən rəhbər yanlışdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

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
