using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Responses.Errors;

public sealed record Error(ErrorType Type, string Message, object? Value = null);