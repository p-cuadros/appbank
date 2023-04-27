using MediatR;
using Microsoft.AspNetCore.Mvc;
using apibanca.application.Commands;
using apibanca.application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace minedu.siagie.v2.Asistencia.Estudiantes.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class accountsController : ControllerBase
    {
        private IMediator _mediator;
        public accountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(Summary = "Obtiene la lista de Asistencias por anio e ie.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("ByUser/{idUser}")]
        public async Task<IActionResult> GetAccountsByUser(int idUser)
        {
            return Ok(await _mediator.Send(new GetAccountsByUserQuery(){ idUser = idUser}));
        }

        [SwaggerOperation(Summary = "Obtiene una Asistencia por ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("ById/{idAccount}")]
        public async Task<IActionResult> GetAccountById(int idAccount)
        {
            return Ok(await _mediator.Send(new GetAccountByIdQuery() { idAccount = idAccount}));
        }

        [SwaggerOperation(
            Summary = "Elimina una Asistencia."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("delete/{idAccount}")]
        public async Task<ActionResult> DeleteAccount(int idAccount)
        {
            await _mediator.Send(new DeleteAccountCommand() { idAccount = idAccount });
            return NoContent();
        }

        [SwaggerOperation(
            Summary = "Agrega una Asistencia."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public async Task<ActionResult<CreateAccountCommandResponse>> PostCreateAccount(
            [FromBody] CreateAccountCommand command)
        {
            var response = await _mediator.Send(command);
            return Created("", response);
        }

        [SwaggerOperation(
            Summary = "Modifica una asistencia detalle."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("deposit")]
        public async Task<ActionResult> PutDepositAccount(
            [FromBody] DepositAccountCommand request
        )
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [SwaggerOperation(
            Summary = "Modifica una asistencia detalle."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("withdraw")]
        public async Task<ActionResult> PutWithdrawAccount(
            [FromBody] WithdrawAccountCommand request
        )
        {
            await _mediator.Send(request);
            return NoContent();
        }

    }
}