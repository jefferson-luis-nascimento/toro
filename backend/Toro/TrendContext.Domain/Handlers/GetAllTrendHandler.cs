using MediatR;
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
    public class GetAllTrendHandler : IRequestHandler<GetAllTrendsRequest, IEnumerable<GetAllTrendsResponse>>
    {
        private readonly IRepository<Trend> repository;

        public GetAllTrendHandler(IRepository<Trend> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<GetAllTrendsResponse>> Handle(GetAllTrendsRequest request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllAsync();

            return result.Select(trend => new GetAllTrendsResponse
            {
                Symbol = trend.Symbol,
                CurrentPrice = trend.CurrentPrice
            }).AsEnumerable();
        }
    }
}
