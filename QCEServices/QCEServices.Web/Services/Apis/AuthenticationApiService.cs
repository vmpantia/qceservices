using System.Text;
using System.Text.Json;
using QCEServices.Shared.Models.Dtos.Authentication;
using QCEServices.Web.Contracts.Services.Apis;

namespace QCEServices.Web.Services.Apis;

public sealed class AuthenticationApiService(HttpClient httpClient, ILogger<AuthenticationApiService> logger) : IAuthenticationApiService
{
    public async Task LoginAsync(LoginDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            HttpContent  content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Authentication/Login", content, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred on user logging-in: {ex.Message} | {ex.InnerException?.Message}");
            throw;
        }
    }

    public async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PostAsync("Authentication/Refresh", null, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred on refreshing token: {ex.Message} | {ex.InnerException?.Message}");
            throw;
        }
    }
}