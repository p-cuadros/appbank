using apibanca.application.Command;
using apibanca.application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apibanca.webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class authorizationController : ControllerBase
{
    private IMediator _mediator;

    public authorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public async Task<ActionResult<LoginCommandResponse>> PostLogin(
        [FromBody] LoginCommand command)
    {
        var response = await _mediator.Send(command);
        return Created("", response);
    }
}
