using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;

namespace TrendContext.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/session")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a new Session.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/session
        ///     {
        ///        "cpf": "99999999999"
        ///     }
        ///
        /// </remarks>
        /// <param name="mediator"></param>
        /// <param name="command"></param>
        /// <returns>A newly user session</returns>
        /// <response code="201">Returns the newly user session</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If has error on server</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromServices] IMediator mediator,
                                                [FromBody] CreateSessionRequest command)
        {
            var result = await mediator.Send(command);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
        }
    }
}
