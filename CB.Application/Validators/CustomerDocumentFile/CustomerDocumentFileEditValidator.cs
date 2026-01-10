

using CB.Application.DTOs.CustomerDocumentFile;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.CustomerDocumentFile
{
    public class CustomerDocumentFileEditValidator : AbstractValidator<CustomerDocumentFileEditDTO>
    {
        public CustomerDocumentFileEditValidator(ICustomerDocumentService staffService)
        {

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file =>
                {
                    if (file == null) return true;

                    var allowedTypes = new[]
                    {
                        "application/pdf",
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "application/vnd.ms-excel",
                        "application/msword",
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    };

                    return allowedTypes.Contains(file.ContentType);
                })
                .WithMessage("Fayl yalnız pdf, xlsx və ya doc formatında ola bilər.");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value != null && v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.CustomerDocumentId)
                .NotNull().WithMessage("Kateqoriya daxil edin")
                .MustAsync(async (staffId, cancellation) =>
                {
                    var staff = await staffService.GetByIdAsync(staffId);
                    return staff != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");
        }
    }
}
