using MediatR;
using Microsoft.AspNetCore.Mvc;
using QCEServices.Application.Users.Commands;
using QCEServices.Shared.Models.Dtos.Users;

namespace QCEServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto request)
    {
        var result = await mediator.Send(new LoginUserCommand(request));
        return Ok(result);
    }
}