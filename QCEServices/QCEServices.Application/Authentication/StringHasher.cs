using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using QCEServices.Domain.Interfaces.Authentication;

namespace QCEServices.Application.Authentication;

public sealed class StringHasher(ILogger<StringHasher> logger) : IStringHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;
    private readonly HashAlgorithmName Alogrithm = HashAlgorithmName.SHA512;

    public string HashValue(string value, string secretKey)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(secretKey);

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(bytes);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while hashing provided value. {ex.Message}");
            throw;
        }
    }

    public string HashPassword(string password)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(password);
            
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Alogrithm, HashSize);

            var hashedPassword = $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
            return hashedPassword;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while hashing provided password. {ex.Message}");
            throw;
        }
    }
    
    public bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(password);
            ArgumentNullException.ThrowIfNull(hashedPassword);
            
            var parts = hashedPassword.Split('-');
            var hash = Convert.FromHexString(parts[0]);
            var salt = Convert.FromHexString(parts[1]);

            var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Alogrithm, HashSize);
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while verifying provided password and hashed password. {ex.Message}");
            return false;
        }
    }
}