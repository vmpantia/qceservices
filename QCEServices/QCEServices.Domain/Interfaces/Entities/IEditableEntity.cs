namespace QCEServices.Domain.Interfaces.Entities;

public interface IEditableEntity : IAuditableEntity
{
    DateTime? ModifiedAt { get; set; }
    string? ModifiedBy { get; set; }
}