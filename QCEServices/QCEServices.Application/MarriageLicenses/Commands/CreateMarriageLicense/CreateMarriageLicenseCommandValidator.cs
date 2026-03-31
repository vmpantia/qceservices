using FluentValidation;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Validators.MarriageLicenses;

namespace QCEServices.Application.MarriageLicenses.Commands.CreateMarriageLicense;

public sealed class CreateMarriageLicenseCommandValidator : AbstractValidator<CreateMarriageLicenseCommand>
{
    public CreateMarriageLicenseCommandValidator(IMarriageLicenseRepository marriageLicenseRepository)
    {
        RuleFor(cml => cml.MarriageLicense).SetValidator(new SaveMarriageLicenseValidator());

        RuleFor(cml => cml.MarriageLicense)
            .MustAsync(async (sml, ct) =>
            {
                // Check if any party (groom or bride) has existing marriage license
                var result = await marriageLicenseRepository.IsExistAsync(
                    expression: ml => (ml.Groom.Name.FirstName == sml.Groom.Name.FirstName && ml.Groom.Name.LastName == sml.Groom.Name.LastName) ||
                                      (ml.Bride.Name.FirstName == sml.Bride.Name.FirstName && ml.Bride.Name.LastName == sml.Bride.Name.LastName),
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("One of the parties have existing marriage license on our system.");
    }
}