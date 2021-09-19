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
    [Route("api/v1/userPosition")]
    public class UserPositionController : ControllerBase
    {
        private readonly ILogger<UserPositionController> _logger;

        public UserPositionController(ILogger<UserPositionController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="id"></param>
        /// <returns>Single user</returns>
        /// <response code="200">Single User</response>
        /// <response code="500">If has error on server</response> 
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((200), Type = typeof(GetAllUsersResponse))]
        [ProducesResponseType((500), Type = typeof(object))]
        public async Task<IActionResult> Get([FromServices] IMediator mediator,
                                                      [FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetByIdUserPositionRequest { Id = id});

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
        }
    }
}
