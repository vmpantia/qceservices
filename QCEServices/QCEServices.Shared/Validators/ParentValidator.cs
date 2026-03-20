using FluentValidation;
using QCEServices.Shared.Enums;
using QCEServices.Shared.Models;

namespace QCEServices.Shared.Validators;

public sealed class ParentValidator : AbstractValidator<Parent>
{
    public ParentValidator()
    {
        RuleFor(p => p.Name).SetValidator(new PersonValidator());
        RuleFor(p => p.Citizenship).NotEmpty().WithMessage("Citizenship is required");
        When(p => p.Status == ParentStatus.Living, () =>
        {
            RuleFor(p => p.Residence)
                .NotNull().WithMessage("Residence is required")
                .SetValidator(new AddressValidator()!);
        });
    }
}