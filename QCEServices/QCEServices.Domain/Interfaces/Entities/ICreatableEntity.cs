namespace QCEServices.Domain.Interfaces.Entities;

public interface ICreatableEntity
{
    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }
}