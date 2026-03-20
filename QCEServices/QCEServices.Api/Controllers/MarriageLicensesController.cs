using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QCEServices.Application.MarriageLicenses.Commands;
using QCEServices.Application.MarriageLicenses.Queries;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;

namespace QCEServices.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class MarriageLicensesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateMarriageLicenseAsync([FromBody] SaveMarriageLicenseDto request)
    {
        var result = await mediator.Send(new CreateMarriageLicenseCommand(request));
        return Ok(result);
    }
    
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