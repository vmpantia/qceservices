using MediatR;
using Microsoft.EntityFrameworkCore;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Authentication;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Responses.Errors;

namespace QCEServices.Application.Authentication.Commands.Refresh;

public sealed class RefreshCommandHandler(ITokenRepository tokenRepository, ITokenProvider tokenProvider) : IRequestHandler<RefreshCommand, Result<AuthTokens>>
{
    public async Task<Result<AuthTokens>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var currentRefreshToken = await tokenRepository.Get(t => t.Value == request.RefreshToken)
            .Include(tbl => tbl.User)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (currentRefreshToken is null || currentRefreshToken.IsRevoked || currentRefreshToken.IsExpired)
            return TokenError.InvalidRefreshToken();
        
        var accessToken = tokenProvider.CreateAccessToken(currentRefreshToken.User);
        var newRefreshToken = tokenProvider.CreateRefreshToken(currentRefreshToken.User);
        
        await SaveTokensAsync(currentRefreshToken, newRefreshToken, cancellationToken);
        return new Result<AuthTokens>(new AuthTokens(accessToken, newRefreshToken));
    }

    private async Task SaveTokensAsync(Token currentRefreshToken, Domain.Authentication.RefreshToken newRefreshToken, CancellationToken cancellationToken)
    {
        currentRefreshToken.IsRevoked = true;
        await tokenRepository.UpdateAsync(currentRefreshToken, cancellationToken);
        await tokenRepository.CreateAsync(currentRefreshToken.User, newRefreshToken, cancellationToken);
    }
}
