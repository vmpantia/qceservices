namespace QCEServices.Domain.Interfaces.Entities;

public interface IEditableEntity
{
    DateTime? ModifiedAt { get; set; }
    string? ModifiedBy { get; set; }
}