namespace QCEServices.Domain.Interfaces.Entities;

public interface IDeletableEntity : IEntity
{ 
    DateTime? DeletedAt { get; set; } 
    string? DeletedBy { get; set; }
}