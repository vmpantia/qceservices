using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Models;

public sealed class Party
{
    public Person Name { get; set; } = new();
    public DateOnly DateOfBirth { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public string Citizenship { get; set; } = string.Empty;
    public string Religion { get; set; } = string.Empty;

    public Place BirthPlace { get; set; } = new();
    public Address Residence { get; set; } = new();
    public Parents Parents { get; set; } = new();
}