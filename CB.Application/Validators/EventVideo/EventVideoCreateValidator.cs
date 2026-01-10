
using CB.Application.DTOs.EventVideo;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.EventVideo
{
    public class EventVideoCreateValidator : AbstractValidator<EventVideoCreateDTO>
    {
        public EventVideoCreateValidator(IEventService eventService)
        {
            RuleFor(x => x.EventId)
                    .NotNull().WithMessage("Tədbiri şəxsi seçin")
                    .MustAsync(async (eventId, cancellation) =>
                    {
                        var entity = await eventService.GetByIdAsync(eventId);
                        return entity != null ? true : false;
                    })
                    .WithMessage("Seçilən tədbir yanlışdır");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Ən azı bir dil üçün url daxil edilməlidir.")
                .MaximumLength(10000)
                .WithMessage("Bu dil üçün url 10000 simvoldan artıq ola bilməz.");
        }
    }
}
