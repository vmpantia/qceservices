using MediatR;
using Microsoft.AspNetCore.Mvc;
using QCEServices.Application.Authentication;
using QCEServices.Application.Authentication.Commands.Login;
using QCEServices.Application.Authentication.Commands.Refresh;
using QCEServices.Domain.Authentication;
using QCEServices.Shared.Enums;
using QCEServices.Shared.Models.Dtos.Authentication;
using QCEServices.Shared.Responses.Errors;

namespace QCEServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto request)
    {
        var result = await mediator.Send(new LoginCommand(request));

        if (!result.IsSuccess)
        {
            return result.Error?.Type switch
            {
                ErrorType.Unauthorized => Unauthorized(result),
                ErrorType.NotFound => NotFound(result),
                _ => BadRequest(result),
            };
        }
        
        SetAuthTokensToCookies(result.Data!);
        return Ok();
    }

    [HttpPost("Refresh")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        if (!Request.Cookies.TryGetValue(RefreshToken.CookieName, out var refreshToken))
            return Unauthorized(TokenError.InvalidRefreshToken());

        var result = await mediator.Send(new RefreshCommand(refreshToken));

        if (!result.IsSuccess)
        {
            return result.Error?.Type switch
            {
                ErrorType.Unauthorized => Unauthorized(result),
                ErrorType.NotFound => NotFound(result),
                _ => BadRequest(result),
            };
        }
        
        SetAuthTokensToCookies(result.Data!);
        return Ok();
    }
    
    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        Response.Cookies.Delete(AccessToken.CookieName);
        Response.Cookies.Delete(RefreshToken.CookieName);
        return Ok();
    }

    private void SetAuthTokensToCookies(AuthTokens authTokens)
    {
        Response.Cookies.Append(
            AccessToken.CookieName,
            authTokens.AccessToken.Value,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = authTokens.AccessToken.Expiration,
                Path = "/"
            });
        
        Response.Cookies.Append(
            RefreshToken.CookieName,
            authTokens.RefreshToken.Value,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = authTokens.RefreshToken.Expiration,
                Path = "/"
            });
    }
}