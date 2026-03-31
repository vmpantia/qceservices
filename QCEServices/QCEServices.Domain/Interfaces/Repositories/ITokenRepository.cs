using QCEServices.Domain.Authentication;
using QCEServices.Domain.Entities;

namespace QCEServices.Domain.Interfaces.Repositories;

public interface ITokenRepository : IBaseRepository<Token>
{
    Task CreateAsync(User user, RefreshToken refreshToken, CancellationToken cancellationToken);
}