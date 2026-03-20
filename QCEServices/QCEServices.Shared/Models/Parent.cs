using QCEServices.Shared.Enums;

namespace QCEServices.Shared.Models;

public sealed class Parent
{
    public Person Name { get; set; } = new();
    public string Citizenship { get; set; } = string.Empty;
    public ParentStatus Status { get; set; }
    public Address? Residence { get; set; }
}