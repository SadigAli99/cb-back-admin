
using CB.Application.DTOs.Auth;
using FluentValidation;

namespace CB.Application.Validators.Auth
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Emaili daxil edin");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parolu daxil edin");
        }
    }
}
