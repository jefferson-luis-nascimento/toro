using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;

namespace TrendContext.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<TrendController> _logger;

        public OrderController(ILogger<TrendController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a new Order.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/order
        ///     {
        ///        "symbol": "PETR4",
        ///        "amount": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="mediator"></param>
        /// <param name="command"></param>
        /// <returns>A newly created trend</returns>
        /// <response code="201">Returns the newly created order</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If has error on server</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromServices] IMediator mediator,
                                                     [FromBody] CreateOrderRequest command)
        {
            var result = await mediator.Send(command);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
        }
    }
}
