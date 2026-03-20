using FluentValidation;
using QCEServices.Shared.Models;

namespace QCEServices.Shared.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(a => a.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(a => a.ProvinceOrState).NotEmpty().WithMessage("Province or State is required");
        RuleFor(a => a.CityOrMunicipality).NotEmpty().WithMessage("City or Municipality is required");
        RuleFor(a => a.Barangay).NotEmpty().WithMessage("Barangay is required");
    }
}