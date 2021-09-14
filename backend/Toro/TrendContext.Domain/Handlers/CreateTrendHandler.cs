using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Data.Interfaces;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repository.Implementations;
using TrendContext.Domain.Repository.Interfaces;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Handlers
{
    public class CreateTrendHandler : IRequestHandler<CreateTrendRequest, CreateTrendResponse>
    {
        private readonly ITrendRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CreateTrendHandler(ITrendRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CreateTrendResponse> Handle(CreateTrendRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var existTrend = await repository.GetBySymbol(request.Symbol);

                if (existTrend != null)
                    return null;

                var trend = new Trend
                {
                    Symbol = request.Symbol,
                    CurrentPrice = request.CurrentPrice,
                };

                repository.Create(trend);
                unitOfWork.Commit();

                return new CreateTrendResponse
                {
                    Id = trend.Id,
                    Symbol = trend.Symbol,
                    CurrentPrice = trend.CurrentPrice,
                };
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return null;
            }
        }
    }
}
