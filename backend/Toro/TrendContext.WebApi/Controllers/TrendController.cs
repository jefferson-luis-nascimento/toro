using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendContext.Domain.Entities;
using TrendContext.Shared.Repository;

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
        public async Task<IEnumerable<Trend>> Get([FromServices] IRepository<Trend> repository)
        {

            return await repository.GetAllAsync();

        }
    }
}
