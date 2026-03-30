using QCEServices.Domain.Entities;

namespace QCEServices.Domain.Interfaces.Authentication;

public interface ITokenProvider
{
    string CreateAccessToken(User user);
    Token CreateRefreshToken(User user);
}