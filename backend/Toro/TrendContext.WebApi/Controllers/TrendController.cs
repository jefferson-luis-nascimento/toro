using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;

namespace TrendContext.WebApi.Controllers
{
    [ApiController]
    [Route("trends")]
    public class TrendController : ControllerBase
    {
        private readonly ILogger<TrendController> _logger;

        public TrendController(ILogger<TrendController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<GetAllTrendResponse>> Get([FromServices] IMediator mediator)
        {
            var result = mediator.Send(new GetAllTrendRequest());

            return result;
        }

        [HttpPost]
        public async Task<CreateTrendResponse> Post([FromServices] IMediator mediator,
                                                    [FromBody] CreateTrendRequest command)
        {
            var result = await mediator.Send(command);

            return result;
        }
    }
}
