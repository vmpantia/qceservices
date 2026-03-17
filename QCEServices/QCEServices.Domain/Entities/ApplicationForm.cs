using QCEServices.Shared.Enums;

namespace QCEServices.Domain.Entities;

public class ApplicationForm : SoftDeleteEntity
{
    public Guid Id { get; set; }
    public ApplicationFormType Type { get; set; }
    public ApplicationFormStatus Status { get; set; }    
}