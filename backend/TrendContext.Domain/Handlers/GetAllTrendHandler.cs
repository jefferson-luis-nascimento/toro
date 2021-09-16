using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Entities;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Handlers
{
    public class GetAllTrendHandler : IRequestHandler<GetAllTrendsRequest, CommandResponse<IEnumerable<GetAllTrendsResponse>>>
    {
        private readonly IRepository<Trend> repository;
        private readonly ILogger<GetAllTrendHandler> logger;

        public GetAllTrendHandler(IRepository<Trend> repository,
            ILogger<GetAllTrendHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<CommandResponse<IEnumerable<GetAllTrendsResponse>>> Handle(GetAllTrendsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.GetAllAsync();

                return new CommandResponse<IEnumerable<GetAllTrendsResponse>>(true, 200, string.Empty,
                    result.Select(trend => new GetAllTrendsResponse
                    {
                        Symbol = trend.Symbol,
                        CurrentPrice = trend.CurrentPrice
                    }).AsEnumerable());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new CommandResponse<IEnumerable<GetAllTrendsResponse>>(false, 500, "Internal Server Error", null);
            }            
        }
    }
}
