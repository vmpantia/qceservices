using QCEServices.Domain.Entities;
using QCEServices.Shared.Enums;

namespace QCEServices.Application.ApplicationForms;

public sealed class ApplicationFormBuilder
{
    private ApplicationFormType _type;
    private ApplicationFormStatus _status;
    private Guid _applicantId;
    
    private ApplicationFormBuilder() { }

    public static ApplicationFormBuilder Empty() => new();

    public ApplicationFormBuilder WithType(ApplicationFormType type)
    {
        _type = type;
        return this;
    }

    public ApplicationFormBuilder WithStatus(ApplicationFormStatus status)
    {
        _status = status;
        return this;
    }

    public ApplicationFormBuilder WithApplicantId(Guid applicantId)
    {
        _applicantId = applicantId;
        return this;
    }

    public ApplicationForm Build() => new ApplicationForm
    {
        Id = Guid.NewGuid(),
        Type = _type,
        Status = _status,
        ApplicantId = _applicantId
    };
}