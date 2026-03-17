namespace QCEServices.Domain.Interfaces.Entities;

public interface ICreatableEntity : IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}