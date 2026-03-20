using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Responses.Errors;

public class UserError
{
    public static Error UsernameOrEmailNotFound() => new(ErrorType.NotFound, "Username or email address is not found in the database.");
    public static Error PasswordIncorrect() => new(ErrorType.Invalid, "User password is incorrect.");
}