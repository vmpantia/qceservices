using QCEServices.Domain.Authentication;
using QCEServices.Domain.Entities;

namespace QCEServices.Domain.Interfaces.Authentication;

public interface ITokenProvider
{
    AccessToken CreateAccessToken(User user);
    RefreshToken CreateRefreshToken(User user);
}