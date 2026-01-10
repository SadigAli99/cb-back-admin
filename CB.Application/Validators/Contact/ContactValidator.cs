
using CB.Application.DTOs.Contact;
using CB.Application.Mappings;
using FluentValidation;

namespace CB.Application.Validators.Contact
{
    public class ContactValidator : AbstractValidator<ContactPostDTO>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactMail)
                .NotEmpty().WithMessage("Email boş olmamalıdır")
                .MaximumLength(100).WithMessage("Email 100 simvoldan çox olmamalıdır");

            RuleFor(x => x.Map)
                .NotEmpty().WithMessage("Xəritə boş olmamalıdır")
                .MaximumLength(10000).WithMessage("Xəritə 10000 simvoldan çox olmamalıdır");

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır")
                .Must(file => file == null || file.ContentType.Contains("application/pdf"))
                .WithMessage("Fayl pdf formatında olmalıdır");

            RuleFor(x => x.Notes)
                .NotEmpty().WithMessage("Ən azı bir dil üçün qeyd daxil edilməlidir.");

            RuleForEach(x => x.Notes)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün qeyd boş ola bilməz.")
                .Must(v => v.Value.Length <= 2000)
                .WithMessage(v => "Bu dil üçün qeyd 2000 simvoldan artıq ola bilməz.");

            RuleFor(x => x.Notes)
                .NotEmpty().WithMessage("Ən azı bir dil üçün qəbul günləri daxil edilməlidir.");

            RuleForEach(x => x.Notes)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage(v => "Bu dil üçün qəbul günləri boş ola bilməz.")
                .Must(v => v.Value.Length <= 2000)
                .WithMessage(v => "Bu dil üçün qəbul günləri 2000 simvoldan artıq ola bilməz.");
        }
    }
}
