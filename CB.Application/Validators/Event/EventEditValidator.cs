using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.Application.DTOs.Event;
using FluentValidation;

namespace CB.Application.Validators.Event
{
    public class EventEditValidator : AbstractValidator<EventEditDTO>
    {
        public EventEditValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarix boş ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 100)
                .WithMessage("Bu dil üçün başlıq 100 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Descriptions)
                .NotEmpty().WithMessage("Ən azı bir dil üçün vəzifə daxil edilməlidir.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value != null && v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
