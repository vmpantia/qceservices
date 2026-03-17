using QCEServices.Domain.Interfaces.Entities;

namespace QCEServices.Domain.Entities;

public abstract class AuditedSoftDeleteEntity : ICreatableEntity, IEditableEntity, IDeletableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
