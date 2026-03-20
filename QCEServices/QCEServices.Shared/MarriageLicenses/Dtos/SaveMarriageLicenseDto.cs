using QCEServices.Shared.Models;

namespace QCEServices.Shared.MarriageLicenses.Dtos;

public sealed class SaveMarriageLicenseDto
{
    public Party Groom { get; set; } = new();
    public Party Bride { get; set; } = new();
}