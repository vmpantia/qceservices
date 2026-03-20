namespace QCEServices.Shared.Models.Dtos.MarriageLicenses;

public sealed class SaveMarriageLicenseDto
{
    public Party Groom { get; set; } = new();
    public Party Bride { get; set; } = new();
}