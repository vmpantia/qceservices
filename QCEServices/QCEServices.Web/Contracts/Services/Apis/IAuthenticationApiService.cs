using QCEServices.Shared.Models.Dtos.Authentication;

namespace QCEServices.Web.Contracts.Services.Apis;

public interface IAuthenticationApiService
{
    Task LoginAsync(LoginDto request,CancellationToken cancellationToken = default);
    Task RefreshAsync(CancellationToken cancellationToken = default);
}