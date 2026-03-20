using FluentValidation;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;

namespace QCEServices.Shared.Validators.MarriageLicenses;

public sealed class SaveMarriageLicenseValidator : AbstractValidator<SaveMarriageLicenseDto>
{
    public SaveMarriageLicenseValidator()
    {
        RuleFor(sml => sml.Bride).SetValidator(new PartyValidator());
        RuleFor(sml => sml.Groom).SetValidator(new PartyValidator());
    }
}