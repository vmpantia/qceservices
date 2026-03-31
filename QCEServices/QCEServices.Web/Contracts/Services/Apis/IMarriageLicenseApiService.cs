using QCEServices.Shared.Models.Dtos.MarriageLicenses;

namespace QCEServices.Web.Contracts.Services.Apis;

public interface IMarriageLicenseApiService
{
    Task<IEnumerable<MarriageLicenseDto>> GetAsync(CancellationToken cancellationToken = default);
}