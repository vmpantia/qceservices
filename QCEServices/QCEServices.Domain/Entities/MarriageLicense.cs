using QCEServices.Domain.Interfaces.Entities;
using QCEServices.Shared.Models;

namespace QCEServices.Domain.Entities;

public class MarriageLicense : IAuditableEntity, IDeletableEntity
{
    public Guid Id { get; set; }
    public Guid ApplicationFormId { get; set; }

    public Party Groom { get; set; } = new();
    public Party Bride { get; set; } = new();
    
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    
    public virtual ApplicationForm ApplicationForm { get; set; }
}
