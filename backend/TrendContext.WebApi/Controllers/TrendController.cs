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
    [Route("api/v1/trends")]
    public class TrendController : ControllerBase
    {
        private readonly ILogger<TrendController> _logger;

        public TrendController(ILogger<TrendController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// List of trends
        /// </summary>
        /// <param name="mediator"></param>
        /// <returns>List of trends</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="500">If has error on server</response> 
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetAllTrendsResponse>))]
        public async Task<IActionResult> Get([FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetAllTrendsRequest());

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
        }

        /// <summary>
        /// Creates a new Trend.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/trends
        ///     {
        ///        "symbol": "PETR3",
        ///        "currentPrice": 31.28
        ///     }
        ///
        /// </remarks>
        /// <param name="mediator"></param>
        /// <param name="command"></param>
        /// <returns>A newly created trend</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If has error on server</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromServices] IMediator mediator,
                                                    [FromBody] CreateTrendRequest command)
        {
            var result = await mediator.Send(command);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
        }
    }
}
