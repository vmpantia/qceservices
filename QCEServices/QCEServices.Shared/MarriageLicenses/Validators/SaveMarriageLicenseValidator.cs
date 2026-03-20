using FluentValidation;
using QCEServices.Shared.MarriageLicenses.Dtos;
using QCEServices.Shared.Validators;

namespace QCEServices.Shared.MarriageLicenses.Validators;

public sealed class SaveMarriageLicenseValidator : AbstractValidator<SaveMarriageLicenseDto>
{
    public SaveMarriageLicenseValidator()
    {
        RuleFor(sml => sml.Bride).SetValidator(new PartyValidator());
        RuleFor(sml => sml.Groom).SetValidator(new PartyValidator());
    }
}