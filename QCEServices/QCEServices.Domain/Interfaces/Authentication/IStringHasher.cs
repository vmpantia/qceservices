namespace QCEServices.Domain.Interfaces.Authentication;

public interface IStringHasher
{
    string HashValue(string value, string secretKey);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}