using QCEServices.Domain.Interfaces.Entities;
using QCEServices.Shared.Enums;
using QCEServices.Shared.Models;

namespace QCEServices.Domain.Entities;

public class User : IAuditableEntity, IDeletableEntity
{
     public Guid Id { get; set; }
     public string Username { get; set; }
     public string Email { get; set; }
     public string Password { get; set; }
     public Person Name { get; set; }
     public UserStatus Status { get; set; }
     
     public DateTime CreatedAt { get; set; }
     public string CreatedBy { get; set; }
     public DateTime? ModifiedAt { get; set; }
     public string? ModifiedBy { get; set; }
     public DateTime? DeletedAt { get; set; }
     public string? DeletedBy { get; set; }
     
     public virtual IList<ApplicationForm> ApplicationForms { get; set; }
}