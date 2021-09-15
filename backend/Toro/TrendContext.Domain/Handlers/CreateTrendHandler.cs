using Flunt.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Data.Interfaces;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repository.Interfaces;

namespace TrendContext.Domain.Handlers
{
    public class CreateTrendHandler : Notifiable<Notification>, IRequestHandler<CreateTrendRequest, CreateTrendResponse>
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
                request.Validate();

                if(!request.IsValid)
                {
                    AddNotifications(request);
                    return null;
                }

                var existTrend = await repository.GetBySymbol(request.Symbol);

                if (existTrend != null)
                {
                    request.AddNotification("Symbol", "Already exists this Symbol.");
                    return null;
                }

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
