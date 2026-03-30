using System.Text;
using System.Text.Json;
using QCEServices.Shared.Models.Dtos.Users;

namespace QCEServices.Web.Authentication;

public sealed class AuthenticationService(HttpClient httpClient, ILogger<AuthenticationService> logger) : IAuthenticationService
{
    public async Task<string> LoginUserAsync(LoginUserDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            HttpContent  content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Login", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<string>(cancellationToken);
            if (string.IsNullOrWhiteSpace(result)) throw new Exception("No token returned");
            
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred on user logging-in: {ex.Message} | {ex.InnerException?.Message}");
            throw;
        }
    }

    public async Task<string> RefreshUserTokenAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PostAsync("Refresh", null, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<string>(cancellationToken);
            if (string.IsNullOrWhiteSpace(result)) throw new Exception("No token returned");
            
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred on refreshing token: {ex.Message} | {ex.InnerException?.Message}");
            throw;
        }
    }
}