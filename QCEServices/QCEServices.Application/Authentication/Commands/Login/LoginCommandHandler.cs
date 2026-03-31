using MediatR;
using QCEServices.Domain.Authentication;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Authentication;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Responses.Errors;

namespace QCEServices.Application.Authentication.Commands.Login;

public sealed class LoginCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository, IStringHasher stringHasher, ITokenProvider tokenProvider) : IRequestHandler<LoginCommand, Result<AuthTokens>>
{
    public async Task<Result<AuthTokens>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetOneAsync(expression: u =>
                u.Username == request.Login.UsernameOrEmail ||
                u.Email == request.Login.UsernameOrEmail,
            cancellationToken);

        if (user is null) 
            return UserError.UsernameOrEmailNotFound();
        
        if (!stringHasher.VerifyPassword(request.Login.Password, user.Password))
            return UserError.PasswordIncorrect();
        
        var accessToken = tokenProvider.CreateAccessToken(user);
        var refreshToken = await GetRefreshTokenAsync(user, cancellationToken);

        await tokenRepository.CreateAsync(user, refreshToken, cancellationToken);
        return new Result<AuthTokens>(new AuthTokens(accessToken, refreshToken));
    }

    private async Task<RefreshToken> GetRefreshTokenAsync(User user, CancellationToken cancellationToken)
    {
        var existingToken = await tokenRepository.GetOneAsync(t => t.UserId == user.Id, cancellationToken);
        if (existingToken is null || existingToken.IsRevoked || existingToken.IsExpired) return tokenProvider.CreateRefreshToken(user);
        return new RefreshToken { Value = existingToken.Value, Expiration = existingToken.ExpiresAt };
    }
}