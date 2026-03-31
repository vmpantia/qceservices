using QCEServices.Shared.Models.Dtos.MarriageLicenses;
using QCEServices.Shared.Responses;
using QCEServices.Web.Contracts.Services.Apis;

namespace QCEServices.Web.Services.Apis;

public sealed class MarriageLicenseApiService(HttpClient httpClient, ILogger<MarriageLicenseApiService> logger) : IMarriageLicenseApiService
{
    public async Task<IEnumerable<MarriageLicenseDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.GetAsync("MarriageLicenses", cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<MarriageLicenseDto>>>(cancellationToken);
            if (result?.Data is null) throw new Exception("Marriage licenses cannot be NULL.");
            
            return result.Data;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred on user logging-in: {ex.Message} | {ex.InnerException?.Message}");
            throw;
        }
    }
}