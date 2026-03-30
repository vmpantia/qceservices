using MediatR;
using Microsoft.AspNetCore.Mvc;
using QCEServices.Application.Users.Commands;
using QCEServices.Shared.Extensions;
using QCEServices.Shared.Models.Dtos.Users;
using QCEServices.Shared.Responses;

namespace QCEServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto request)
    {
        var result = await mediator.Send(new LoginUserCommand(request));

        if (result.IsSuccess)
        {
            Response.Cookies.Append("refresh_token", 
                result.Data.RefreshToken.Value, 
                new CookieOptions {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.Data.RefreshToken.ExpiresAt
            });
        
            return Ok(new Result<string>(result.Data.AccessToken));
        }

        return BadRequest(result);
    }
    
    [HttpPost("Refresh")]
    public async Task<IActionResult> RefreshUserTokenAsync()
    {
        var user = HttpContext.User.GetEmail();
        var refreshToken = HttpContext.Request.Cookies["refresh_token"] ?? string.Empty;
        
        var result = await mediator.Send(new RefreshUserTokenCommand(user, refreshToken));

        if (result.IsSuccess)
        {
            Response.Cookies.Append("refresh_token", 
                result.Data.RefreshToken.Value, 
                new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Data.RefreshToken.ExpiresAt
                });
        
            return Ok(new Result<string>(result.Data.AccessToken));
        }

        return BadRequest(result);
    }
}