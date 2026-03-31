using FluentValidation;
using QCEServices.Shared.Models.Dtos.Authentication;

namespace QCEServices.Shared.Validators.Authentication;

public sealed class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(lud => lud.UsernameOrEmail).NotEmpty().WithMessage("Username or email is required.");
        RuleFor(lud => lud.Password).NotEmpty().WithMessage("Password is required.");
    }
}