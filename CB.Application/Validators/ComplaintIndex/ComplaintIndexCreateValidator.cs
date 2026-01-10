
using CB.Application.DTOs.ComplaintIndex;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.ComplaintIndex
{
    public class ComplaintIndexCreateValidator : AbstractValidator<ComplaintIndexCreateDTO>
    {
        public ComplaintIndexCreateValidator(IComplaintIndexCategoryService complaintIndexCategoryService)
        {

            RuleFor(x => x.Month)
                .NotNull().WithMessage("Ayı daxil edin")
                .GreaterThanOrEqualTo(1).WithMessage("Ayı düzgün daxil edin")
                .LessThanOrEqualTo(12).WithMessage("Ayı düzgün daxil edin");

            RuleFor(x => x.Year)
                .NotNull().WithMessage("İli daxil edin")
                .GreaterThanOrEqualTo(1990).WithMessage("İlin dəyəri 1990-dan kiçik ola bilməz");

            RuleFor(x => x.File)
                .NotNull().WithMessage("Faylı seçmək tələb olunur")
                .Must(file => file != null && file.Length / 1024 <= 10000)
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

            RuleFor(x => x.CoverTitles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün fayl başlığı daxil edilməlidir.");

            RuleForEach(x => x.CoverTitles)
                .Must(v => v.Value == null || v.Value.Length <= 500)
                .WithMessage("Bu dil üçün fayl başlığı 500 simvoldan artıq ola bilməz.");

            RuleFor(x => x.ComplaintIndexCategoryId)
                .NotNull().WithMessage("Kateqoriya daxil edin")
                .MustAsync(async (complaintIndexCategoryId, cancellation) =>
                {
                    var complaintIndexCategory = await complaintIndexCategoryService.GetByIdAsync(complaintIndexCategoryId);
                    return complaintIndexCategory != null ? true : false;
                })
                .WithMessage("Seçilən kateqoriya yanlışdır");


        }
    }
}
