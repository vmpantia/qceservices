namespace QCEServices.Shared.Models.Dtos.Users;

public sealed class LoginUserDto
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}