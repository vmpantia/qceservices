using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QCEServices.Application.ApplicationForms.Commands;

namespace QCEServices.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class ApplicationFormsController(IMediator mediator) : ControllerBase
{
    [HttpPost("{id}/Submit")]
    public async Task<IActionResult> CreateMarriageLicenseAsync(Guid id)
    {
        var result = await mediator.Send(new SubmitApplicationFormCommand(id));
        return Ok(result);
    }
}    
