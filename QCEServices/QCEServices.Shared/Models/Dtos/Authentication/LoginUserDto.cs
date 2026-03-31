namespace QCEServices.Shared.Models.Dtos.Authentication;

public sealed class LoginDto
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}