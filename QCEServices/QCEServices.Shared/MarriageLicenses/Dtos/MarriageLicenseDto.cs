using QCEServices.Shared.ApplicationForms.Dtos;
using QCEServices.Shared.Models;

namespace QCEServices.Shared.MarriageLicenses.Dtos;

public sealed class MarriageLicenseDto
{
    public Guid Id { get; set; }
    public Party Groom { get; set; }
    public Party Bride { get; set; }
    public ApplicationFormDto ApplicationForm { get; set; }
    
    public DateTime LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}