namespace QCEServices.Domain.Interfaces.Entities;

public interface IDeletableEntity : IAuditableEntity
{
    DateTime? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
}