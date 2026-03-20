using FluentValidation;
using QCEServices.Shared.Models;

namespace QCEServices.Shared.Validators;

public sealed class PartyValidator : AbstractValidator<Party>
{
    public PartyValidator()
    {
        RuleFor(p => p.Name).SetValidator(new PersonValidator());
        RuleFor(p => p.DateOfBirth).NotEmpty().WithMessage("Birthday is required.");
        RuleFor(p => p.CivilStatus).NotEmpty().WithMessage("Civil status is required");
        RuleFor(p => p.Citizenship).NotEmpty().WithMessage("Citizenship is required");
        RuleFor(p => p.Religion).NotEmpty().WithMessage("Religion is required");
        
        RuleFor(p => p.BirthPlace).SetValidator(new PlaceValidator());
        RuleFor(p => p.Residence).SetValidator(new AddressValidator());
        RuleFor(p => p.Parents.Father).SetValidator(new ParentValidator());
        RuleFor(p => p.Parents.Mother).SetValidator(new ParentValidator());
    }
}