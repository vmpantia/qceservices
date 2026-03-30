using System.ComponentModel.DataAnnotations.Schema;
using QCEServices.Domain.Interfaces.Entities;

namespace QCEServices.Domain.Entities;

public class Token : IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Value { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiresAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    
    public User User { get; set; }
    
    [NotMapped]
    public bool IsExpired => ExpiresAt < DateTime.UtcNow;
}