using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// List of users
        /// </summary>
        /// <param name="mediator"></param>
        /// <returns>List of trends</returns>
        /// <response code="200">Returns the list of users</response>
        /// <response code="500">If has error on server</response> 
        [HttpGet]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetAllUsersResponse>))]
        [ProducesResponseType((500), Type = typeof(object))]
        public async Task<IActionResult> Get([FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetAllUsersRequest());

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
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
        [Authorize]
        [ProducesResponseType((200), Type = typeof(GetAllUsersResponse))]
        [ProducesResponseType((500), Type = typeof(object))]
        public async Task<IActionResult> Get([FromServices] IMediator mediator,
                                                      [FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetByIdUserRequest { Id = id});

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/users
        ///     {
        ///        "name": "Name of User",
        ///        "cpf": "99999999999"
        ///     }
        ///
        /// </remarks>
        /// <param name="mediator"></param>
        /// <param name="command"></param>
        /// <returns>A newly created user</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If has error on server</response> 
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromServices] IMediator mediator,
                                                     [FromBody] CreateUserRequest command)
        {
            var result = await mediator.Send(command);

            return StatusCode(result.StatusCode, result.Success ? result.Payload : new { message = result.Message });
        }
    }
}
