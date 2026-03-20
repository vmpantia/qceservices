using QCEServices.Application.ApplicationForms;
using QCEServices.Domain.Entities;

namespace QCEServices.Application.MarriageLicenses;

public sealed class MarriageLicenseBuilder
{
    private MarriageLicense _marriageLicense;
    private ApplicationFormBuilder _applicationFormBuilder;
    
    private MarriageLicenseBuilder() { }
    
    public static MarriageLicenseBuilder Empty() => new();

    public MarriageLicenseBuilder WithMarriageLicense(MarriageLicense marriageLicense)
    {
        _marriageLicense = marriageLicense;
        return this;
    }

    public MarriageLicenseBuilder WithApplicationForm(ApplicationFormBuilder applicationFormBuilder)
    {
        _applicationFormBuilder = applicationFormBuilder;
        return this;
    }

    public MarriageLicense Build()
    {
        _marriageLicense.ApplicationForm = _applicationFormBuilder.Build();
        return _marriageLicense;
    }
}