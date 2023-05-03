using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shipping.API.Application.Commands;
using Shipping.API.Application.Models;

namespace Shipping.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Consumes("application/json")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{vehiclePlate}/distribute")]
        public async Task<IActionResult> Distribute(string vehiclePlate, [FromBody] DistributeRequest requestBody)
        {
            if (vehiclePlate == default || requestBody is null) { return BadRequest(); }

            var distributeCommand = DistributeCommand.FromRequest(vehiclePlate, requestBody);

            var result = await _mediator.Send(distributeCommand);

            return Ok(result);
        }
    }
}