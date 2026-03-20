namespace QCEServices.Shared.Models;

public sealed class Parents
{
    public Parent Father { get; set; } = new();
    public Parent Mother { get; set; } = new();
}