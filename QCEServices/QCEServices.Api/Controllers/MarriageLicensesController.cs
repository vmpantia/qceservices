using MediatR;
using Microsoft.AspNetCore.Mvc;
using QCEServices.Application.MarriageLicenses.Queries;

namespace QCEServices.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MarriageLicensesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMarriageLicensesAsync()
    {
        var result = await mediator.Send(new GetMarriageLicensesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMarriageLicenseByIdAsync(Guid id)
    {
        var result = await mediator.Send(new GetMarriageLicenseByIdQuery(id));
        return Ok(result);
    }
}