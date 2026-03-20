using QCEServices.Shared.Models.Dtos.ApplicationForms;

namespace QCEServices.Shared.Models.Dtos.MarriageLicenses;

public sealed class MarriageLicenseDto
{
    public Guid Id { get; set; }
    public Party Groom { get; set; }
    public Party Bride { get; set; }
    public ApplicationFormDto ApplicationForm { get; set; }
    
    public DateTime LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}