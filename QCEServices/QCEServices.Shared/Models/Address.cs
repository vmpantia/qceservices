namespace QCEServices.Shared.Models;

public sealed class Address : Place
{
    public string Barangay { get; set; } = string.Empty;
    public string? HouseNoOrStreet { get; set; }
}