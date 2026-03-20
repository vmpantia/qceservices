using System.Security.Claims;

namespace QCEServices.Shared.Extensions;

public static class AuthExtension
{
    extension(ClaimsPrincipal user)
    {
        public string GetEmail() =>
            user.GetClaimValue(ClaimTypes.Email);

        public Guid GetUpn()
        {
            var upn = user.GetClaimValue(ClaimTypes.Upn);
            return Guid.TryParse(upn, out var upnGuid) ? upnGuid : Guid.Empty;
        }
        
        public string GetClaimValue(string claimType) =>
            user.Claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? string.Empty;
    }
}