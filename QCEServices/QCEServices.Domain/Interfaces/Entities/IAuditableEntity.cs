namespace QCEServices.Domain.Interfaces.Entities;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
    DateTime? ModifiedAt { get; set; }
    string? ModifiedBy { get; set; }
}