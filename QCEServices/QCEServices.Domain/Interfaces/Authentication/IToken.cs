namespace QCEServices.Domain.Interfaces.Authentication;

public interface IToken
{
    string Value { get; set; }
    DateTime Expiration { get; set; }
}