using FluentValidation;
using MediatR;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces;
using QCEServices.Domain.Interfaces.Authentication;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Models.Dtos.Users;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Responses.Errors;
using QCEServices.Shared.Validators.Users;

namespace QCEServices.Application.Users.Commands;

public sealed record LoginUserCommand(LoginUserDto Login) : IRequest<Result<(string AccessToken, Token RefreshToken)>>, ICommand;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(lu => lu.Login).SetValidator(new LoginUserValidator());
    }
}

public sealed class LoginUserCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository,
    IStringHasher stringHasher, ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<(string AccessToken, Token RefreshToken)>>
{
    public async Task<Result<(string AccessToken, Token RefreshToken)>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetOneAsync(expression: u =>
                u.Username == request.Login.UsernameOrEmail ||
                u.Email == request.Login.UsernameOrEmail,
            cancellationToken);

        if (user is null) return UserError.UsernameOrEmailNotFound();
        
        if (!stringHasher.VerifyPassword(request.Login.Password, user.Password)) return UserError.PasswordIncorrect();

        var accessToken = tokenProvider.CreateAccessToken(user);
        
        var refreshToken = tokenProvider.CreateRefreshToken(user);
        await tokenRepository.CreateAsync(refreshToken, cancellationToken);
        
        return (accessToken, refreshToken);
    }
}
