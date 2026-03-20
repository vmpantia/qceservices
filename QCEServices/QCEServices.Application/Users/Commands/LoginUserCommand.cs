using FluentValidation;
using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Domain.Interfaces.Authentication;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Models.Dtos.Users;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Responses.Errors;
using QCEServices.Shared.Validators.Users;

namespace QCEServices.Application.Users.Commands;

public sealed record LoginUserCommand(LoginUserDto Login) : IRequest<Result<string>>, ICommand;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(lu => lu.Login).SetValidator(new LoginUserValidator());
    }
}

public sealed class LoginUserCommandHandler(IUserRepository userRepository, 
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetOneAsync(expression: u =>
                u.Username == request.Login.UsernameOrEmail ||
                u.Email == request.Login.UsernameOrEmail,
            cancellationToken);

        if (user is null) return UserError.UsernameOrEmailNotFound();
        
        if (!passwordHasher.Verify(request.Login.Password, user.Password)) return UserError.PasswordIncorrect();

        return tokenProvider.Create(user);
    }
}
