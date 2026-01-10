
using CB.Application.DTOs.Blog;
using FluentValidation;

namespace CB.Application.Validators.Blog
{
    public class BlogCreateValidator : AbstractValidator<BlogCreateDTO>
    {
        public BlogCreateValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarix boş ola bilməz.")
                .Must(d => d != default).WithMessage("Tarix düzgün formatda olmalıdır (yyyy-MM-dd).");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("Zəhmət olmasa, faylı seçin")
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");

            RuleForEach(x => x.Files)
                .Must(file => file != null && file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file != null && file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır.");

            RuleFor(x => x.Titles)
                .NotEmpty().WithMessage("Ən azı bir dil üçün başlıq daxil edilməlidir.");

            RuleForEach(x => x.Titles)
                .Must(v => !string.IsNullOrWhiteSpace(v.Value))
                .WithMessage("Bu dil üçün başlıq boş ola bilməz.")
                .Must(v => v.Value.Length <= 500)
                .WithMessage("Bu dil üçün başlıq 500 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.ImageAlts)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün şəkil alt atributu 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.ImageTitles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün şəkil başlıq 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaTitles)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün meta başlıq 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaDescriptions)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün meta təsvir 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.MetaKeywords)
                .Must(v => v.Value.Length <= 255)
                .WithMessage("Bu dil üçün meta keyword 255 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.ShortDescriptions)
                .Must(v => v.Value.Length <= 1000)
                .WithMessage("Bu dil üçün qısa mətn 1000 simvoldan artıq ola bilməz.");

            RuleForEach(x => x.Descriptions)
                .Must(v => v.Value.Length <= 50000)
                .WithMessage("Bu dil üçün mətn 50000 simvoldan artıq ola bilməz.");
        }
    }
}
