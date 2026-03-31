using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Responses.Errors;

public abstract class ApplicationFormError
{
    public static Error NotFound(Guid id) => new(ErrorType.NotFound, $"Application form with an '{id}' Id is not exist on the system.");
}