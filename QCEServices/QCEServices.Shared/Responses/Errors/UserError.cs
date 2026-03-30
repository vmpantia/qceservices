using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Responses.Errors;

public class UserError
{
    public static Error UsernameOrEmailNotFound() => new(ErrorType.NotFound, "Username or email address is not found in the database.");
    public static Error UserNotFound() => new(ErrorType.NotFound, "User is not found in the database.");
    public static Error PasswordIncorrect() => new(ErrorType.Invalid, "User password is incorrect.");
    public static Error InvalidUserRefreshToken() => new(ErrorType.Unauthorized, "User uses invalid or expired refresh token.");
}