using MediatR;
using Microsoft.AspNetCore.Mvc;
using QCEServices.Application.Authentication.Commands.Login;
using QCEServices.Shared.Models.Dtos.Authentication;

namespace QCEServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto request)
    {
        var result = await mediator.Send(new LoginCommand(request));
        return Ok(result);
    }
}