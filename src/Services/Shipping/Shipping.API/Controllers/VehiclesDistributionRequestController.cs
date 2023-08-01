using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shipping.API.Application.Commands;

namespace Shipping.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]/{vehiclePlate}")]
    [Consumes("application/json")]
    public class VehiclesDistributionRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesDistributionRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("distribution-request")]
        public async Task<IActionResult> Distribute(string vehiclePlate, [FromBody] DistributeCommand command)
        {
            if (vehiclePlate == default || command is null) { return BadRequest(); }

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}