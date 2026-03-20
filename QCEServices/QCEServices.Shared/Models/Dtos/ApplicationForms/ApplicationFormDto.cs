using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Models.Dtos.ApplicationForms;

public sealed class ApplicationFormDto
{
    public Guid Id { get; set; }
    public ApplicationFormType Type { get; set; }
    public ApplicationFormStatus Status { get; set; }
    
    public DateTime? SubmittedAt { get; set; }
    public string? SubmittedBy { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}