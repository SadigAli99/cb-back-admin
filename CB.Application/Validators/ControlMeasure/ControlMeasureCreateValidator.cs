using CB.Application.DTOs.ControlMeasure;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.ControlMeasure
{
    public class ControlMeasureCreateValidator : AbstractValidator<ControlMeasureCreateDTO>
    {
        public ControlMeasureCreateValidator(IControlMeasureCategoryService controlMeasureCategoryService)
        {

            RuleFor(x => x.Month)
                .NotNull().WithMessage("Ayı daxil edin")
                .GreaterThanOrEqualTo(1).WithMessage("Ayı düzgün daxil edin")
                .LessThanOrEqualTo(12).WithMessage("Ayı düzgün daxil edin");

            RuleFor(x => x.Year)
                .NotNull().WithMessage("İli daxil edin")
                .GreaterThanOrEqualTo(1990).WithMessage("İlin dəyəri 1990-dan kiçik ola bilməz");

            RuleFor(x => x.ControlMeasureCategoryId)
                .NotNull().WithMessage("Kateqoriya daxil edin")
                .MustAsync(async (controlMeasureCategoryId, cancellation) =>
                {
                    var controlMeasureCategory = await controlMeasureCategoryService.GetByIdAsync(controlMeasureCategoryId);
                    return controlMeasureCategory != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 10000)
                .WithMessage("Bu dil üçün mətn 10000 simvoldan artıq ola bilməz.");
        }
    }
}
