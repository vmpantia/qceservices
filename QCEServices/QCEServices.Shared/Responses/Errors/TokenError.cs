using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Responses.Errors;

public abstract class TokenError
{
    public static Error InvalidRefreshToken() => new(ErrorType.Unauthorized, "Invalid or expired refresh token.");
}