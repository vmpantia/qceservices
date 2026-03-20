using FluentValidation;
using QCEServices.Shared.Models;

namespace QCEServices.Shared.Validators;

public sealed class PlaceValidator : AbstractValidator<Place>
{
    public PlaceValidator()
    {
        RuleFor(p => p.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(p => p.ProvinceOrState).NotEmpty().WithMessage("Province or State is required");
        RuleFor(p => p.CityOrMunicipality).NotEmpty().WithMessage("City or Municipality is required");
    }
}