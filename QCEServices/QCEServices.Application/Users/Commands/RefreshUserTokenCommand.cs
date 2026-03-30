using MediatR;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces;
using QCEServices.Domain.Interfaces.Authentication;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Responses.Errors;

namespace QCEServices.Application.Users.Commands;

public sealed record RefreshUserTokenCommand(string User, string RefreshToken) : IRequest<Result<(string AccessToken, Token RefreshToken)>>, ICommand;

public sealed class RefreshUserTokenCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository, ITokenProvider tokenProvider) : IRequestHandler<RefreshUserTokenCommand, Result<(string AccessToken, Token RefreshToken)>>
{
    public async Task<Result<(string AccessToken, Token RefreshToken)>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetOneAsync(expression: u =>
                u.Username == request.User || u.Email == request.User,
            cancellationToken);

        if (user is null) return UserError.UserNotFound();
        
        var refreshToken = await tokenRepository.GetOneAsync(
            expression: t => t.Value == request.RefreshToken && t.UserId == user.Id,      
            cancellationToken);

        if (refreshToken == null || refreshToken.IsRevoked || refreshToken.IsExpired)
            return UserError.InvalidUserRefreshToken();
        
        refreshToken.IsRevoked = false;
        await tokenRepository.UpdateAsync(refreshToken, cancellationToken);
        
        var newRefreshToken = tokenProvider.CreateRefreshToken(user);
        await tokenRepository.CreateAsync(newRefreshToken, cancellationToken);

        var accessToken = tokenProvider.CreateAccessToken(user);
        
        return (accessToken, refreshToken);
    }
}
