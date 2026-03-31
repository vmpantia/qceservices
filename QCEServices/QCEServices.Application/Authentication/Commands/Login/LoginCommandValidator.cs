using FluentValidation;
using QCEServices.Shared.Validators.Authentication;

namespace QCEServices.Application.Authentication.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(lu => lu.Login).SetValidator(new LoginValidator());
    }
}