using FluentValidation;
using QCEServices.Shared.Models;

namespace QCEServices.Shared.Validators;

public sealed class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required");
    }
}