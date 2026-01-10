

using CB.Application.DTOs.MetalMoney;
using FluentValidation;

namespace CB.Application.Validators.MetalMoney
{
    public class MetalMoneyEditValidator : AbstractValidator<MetalMoneyEditDTO>
    {
        public MetalMoneyEditValidator()
        {
            RuleFor(x => x.File)
                .Must(file => file == null || file.Length / 1024 <= 10000)
                .WithMessage("Faylın ölçüsü 10 MB-dan çox olmamalıdır.")
                .Must(file => file == null || file.ContentType.Contains("image"))
                .WithMessage("Fayl şəkil formatında olmalıdır");
        }
    }
}
