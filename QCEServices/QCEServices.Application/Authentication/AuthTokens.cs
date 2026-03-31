
using QCEServices.Domain.Authentication;

namespace QCEServices.Application.Authentication;

public record AuthTokens(AccessToken AccessToken, RefreshToken RefreshToken);