

using CB.Application.DTOs.EventVideo;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.EventVideo
{
    public class EventVideoEditValidator : AbstractValidator<EventVideoEditDTO>
    {
        public EventVideoEditValidator(IDirectorService directorService)
        {
            RuleFor(x => x.EventId)
                    .NotNull().WithMessage("Tədbiri şəxsi seçin")
                    .MustAsync(async (directorId, cancellation) =>
                    {
                        var director = await directorService.GetByIdAsync(directorId);
                        return director != null ? true : false;
                    })
                    .WithMessage("Seçilən rəhbər yanlışdır");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Ən azı bir dil üçün url daxil edilməlidir.")
                .MaximumLength(10000)
                .WithMessage("Bu dil üçün url 10000 simvoldan artıq ola bilməz.");
        }
    }
}
