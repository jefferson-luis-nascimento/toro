using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Data.Interfaces;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repositories.Interfaces;

namespace TrendContext.Domain.Handlers
{
    public class CreateTrendHandler : Notifiable<Notification>, IRequestHandler<CreateTrendRequest, CommandResponse<CreateTrendResponse>>
    {
        private readonly ITrendRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CreateTrendHandler> logger;

        public CreateTrendHandler(ITrendRepository repository, 
            IUnitOfWork unitOfWork,
            ILogger<CreateTrendHandler> logger)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<CommandResponse<CreateTrendResponse>> Handle(CreateTrendRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();

                if(!request.IsValid)
                {
                    return new CommandResponse<CreateTrendResponse>(false, 400, string.Join("<br />", request.Notifications.Select(x => x.Message)), null);
                }

                var existTrend = await repository.GetBySymbol(request.Symbol);

                if (existTrend != null)
                {
                    return new CommandResponse<CreateTrendResponse>(false, 400, "Already exists this Trend.", null);
                }

                var trend = new Trend
                {
                    Symbol = request.Symbol,
                    CurrentPrice = request.CurrentPrice,
                };

                unitOfWork.BeginTransaction();

                repository.Create(trend);
                await unitOfWork.Commit();

                return new CommandResponse<CreateTrendResponse>(true, 201, string.Empty,
                    new CreateTrendResponse
                    {
                        Id = trend.Id,
                        Symbol = trend.Symbol,
                        CurrentPrice = trend.CurrentPrice,
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                unitOfWork.Rollback();
                return new CommandResponse<CreateTrendResponse>(false, 500, "Internal Server Error", null);
            }
        }
    }
}
