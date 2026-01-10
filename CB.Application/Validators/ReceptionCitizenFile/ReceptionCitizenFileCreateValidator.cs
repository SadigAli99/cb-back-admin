
using CB.Application.DTOs.ReceptionCitizenFile;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.ReceptionCitizenFile
{
    public class ReceptionCitizenFileCreateValidator : AbstractValidator<ReceptionCitizenFileCreateDTO>
    {
        public ReceptionCitizenFileCreateValidator(IReceptionCitizenService receptionCitizenService)
        {
            RuleFor(x => x.ReceptionCitizenId)
                .MustAsync(async (ReceptionCitizenID, cancellation) =>
                {
                    var statisticalReportCategory = await receptionCitizenService.GetByIdAsync(ReceptionCitizenID);
                    return statisticalReportCategory != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");


            RuleFor(x => x.File)
                .NotNull().WithMessage("Fayl seçilməyib")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("application/pdf"))
                .WithMessage("Fayl pdf formatında olmalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");
        }
    }
}
