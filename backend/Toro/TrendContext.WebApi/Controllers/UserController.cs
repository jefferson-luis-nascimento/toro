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
        /// <response code="200">Returns the newly created item</response>
        /// <response code="500">If has error on server</response> 
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetAllUsersResponse>))]
        public async Task<IActionResult> GetAllAsync([FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetAllUsersRequest());

            return Ok(result);
        }
    }
}
