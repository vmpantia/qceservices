using QCEServices.Domain.Entities;

namespace QCEServices.Domain.Interfaces.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}