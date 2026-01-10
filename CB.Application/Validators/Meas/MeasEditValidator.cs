
using CB.Application.DTOs.Meas;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.Meas
{
    public class MeasEditValidator : AbstractValidator<MeasEditDTO>
    {
        public MeasEditValidator(
            IIssuerTypeService issuerTypeService,
            IInformationTypeService informationTypeService,
            ISecurityTypeService securityTypeService
        )
        {
            RuleFor(x => x.RegisterTime)
                .NotNull().WithMessage("Tarix boş ola bilməz");

            RuleFor(x => x.RegisterNumber)
                .NotNull().WithMessage("DQN boş ola bilməz");

            RuleFor(x => x.Titles)
                .NotEmpty()
                .WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır")
                .Must(file => file == null || file.ContentType.Contains("application/pdf"))
                .WithMessage("Fayl pdf formatında olmalıdır");

            RuleFor(x => x.IssuerTypeId)
                .NotNull().WithMessage("Emitent növünü daxil edin")
                .MustAsync(async (issuerTypeId, cancellation) =>
                {
                    var issuerType = await issuerTypeService.GetByIdAsync(issuerTypeId);
                    return issuerType != null ? true : false;
                })
                .WithMessage("Seçilən emitent növü yanlışdır");

            RuleFor(x => x.InformationTypeId)
                .NotNull().WithMessage("Məlumat növünü daxil edin")
                .MustAsync(async (informationTypeId, cancellation) =>
                {
                    var informationType = await informationTypeService.GetByIdAsync(informationTypeId);
                    return informationType != null ? true : false;
                })
                .WithMessage("Seçilən məlumat növü yanlışdır");

            RuleFor(x => x.SecurityTypeId)
                .NotNull().WithMessage("Qiymətli kağız növünü daxil edin")
                .MustAsync(async (securityTypeId, cancellation) =>
                {
                    var securityType = await securityTypeService.GetByIdAsync(securityTypeId);
                    return securityType != null ? true : false;
                })
                .WithMessage("Seçilən qiymətli kağız növü yanlışdır");
        }

    }
}
