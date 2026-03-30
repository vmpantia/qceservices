using QCEServices.Shared.Models.Dtos.Users;

namespace QCEServices.Web.Authentication;

public interface IAuthenticationService
{
    Task<string> LoginUserAsync(LoginUserDto request,CancellationToken cancellationToken = default);
    Task<string> RefreshUserTokenAsync(CancellationToken cancellationToken = default);
}