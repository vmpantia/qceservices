using QCEServices.Domain.Interfaces.Entities;
using QCEServices.Shared.Enums;

namespace QCEServices.Domain.Entities;

public class ApplicationForm : IAuditableEntity, IDeletableEntity
{
    public Guid Id { get; set; }
    public ApplicationFormType Type { get; set; }
    public ApplicationFormStatus Status { get; set; }
    
    public DateTime? SubmittedAt { get; set; }
    public string? SubmittedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    
    public virtual MarriageLicense MarriageLicense { get; set; }
}