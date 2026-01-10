

using CB.Application.DTOs.PageDetail;
using CB.Application.Interfaces.Services;
using FluentValidation;

namespace CB.Application.Validators.PageDetail
{
    public class PageDetailEditValidator : AbstractValidator<PageDetailEditDTO>
    {
        public PageDetailEditValidator(
            IPageService pageService
        )
        {

            RuleFor(x => x.PageId)
                    .MustAsync(async (pageID, cancellation) =>
                    {
                        var page = await pageService.GetByIdAsync(pageID);
                        return page != null ? true : false;
                    })
                    .WithMessage("Seçilən səhifə yanlışdır");

            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("Açar boş ola bilməz")
                .MaximumLength(200).WithMessage("Açar uzunluğu 200 simvoldan artıq ola bilməz");

            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Urls)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün link 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaTitles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün meta başlıq 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaDescriptions)
                .Must(v => v.Value.Length <= 255)
                .WithMessage(v => $"Bu dil üçün başlıq 255 simvoldan artıq ola bilməz.");
        }
    }
}
