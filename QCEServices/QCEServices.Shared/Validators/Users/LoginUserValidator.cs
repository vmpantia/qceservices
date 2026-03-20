using FluentValidation;
using QCEServices.Shared.Models.Dtos.Users;

namespace QCEServices.Shared.Validators.Users;

public sealed class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(lud => lud.UsernameOrEmail).NotEmpty().WithMessage("Username or email is required.");
        RuleFor(lud => lud.Password).NotEmpty().WithMessage("Password is required.");
    }
}