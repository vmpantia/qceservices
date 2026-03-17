namespace QCEServices.Domain.Interfaces.Entities;

public interface IDeletableEntity
{
    DateTime? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
}