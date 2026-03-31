using QCEServices.Domain.Authentication;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Infrastructure.DataAccess.Contexts;

namespace QCEServices.Infrastructure.DataAccess.Repositories;

public sealed class TokenRepository(QCEServicesDbContext context) : BaseRepository<Token>(context), ITokenRepository
{
    public async Task CreateAsync(User user, RefreshToken refreshToken, CancellationToken cancellationToken) =>
        await CreateAsync(new Token
        {
            UserId = user.Id,
            Value = refreshToken.Value,
            IsRevoked = false,
            ExpiresAt = refreshToken.Expiration
        }, cancellationToken);
}