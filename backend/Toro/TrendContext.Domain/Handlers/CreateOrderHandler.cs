using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Data.Interfaces;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repository.Interfaces;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Handlers
{
    public class CreateOrderHandler : Notifiable<Notification>, IRequestHandler<CreateOrderRequest, CommandResponse<CreateOrderResponse>>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly ITrendRepository trendRepository;
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CreateOrderHandler> logger;

        public CreateOrderHandler(IRepository<Order> orderRepository,
            ITrendRepository trendRepository,
            IRepository<User> userRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreateOrderHandler> logger)
        {
            this.orderRepository = orderRepository;
            this.trendRepository = trendRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<CommandResponse<CreateOrderResponse>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();

                if (!request.IsValid)
                {
                    return new CommandResponse<CreateOrderResponse>(false, 400,
                        JsonConvert.SerializeObject(request.Notifications.Select(x => new { x.Key, x.Message })), null);
                }

                var existingTrend = await trendRepository.GetBySymbol(request.Symbol);

                if (existingTrend == null)
                {
                    return new CommandResponse<CreateOrderResponse>(false, 404, "Trend not found.", null);
                }

                var existingUser = await userRepository.GetByIdAsync(request.UserId);

                if (existingUser == null)
                {
                    return new CommandResponse<CreateOrderResponse>(false, 404, "User not found.", null);
                }

                var totalOrder = Math.Round(existingTrend.CurrentPrice * request.Amount, 2, MidpointRounding.AwayFromZero);

                if (totalOrder > existingUser.CheckingAccountAmount)
                {
                    return new CommandResponse<CreateOrderResponse>(false, 400, "Insufficient funds.", null);
                }

                var order = new Order
                {
                    TrendId = existingTrend.Id,
                    UserId = request.UserId,
                    Amount = request.Amount,
                };

                orderRepository.Create(order);

                existingUser.CheckingAccountAmount -= totalOrder;
                await userRepository.UpdateAsync(existingUser);

                unitOfWork.Commit();

                return new CommandResponse<CreateOrderResponse>(true, 201, string.Empty, 
                    new CreateOrderResponse
                    {
                        Id = order.Id,
                        Symbol = existingTrend.Symbol,
                        Amount = order.Amount,
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                unitOfWork.Rollback();
                return new CommandResponse<CreateOrderResponse>(false, 500, "Internal Server Error", null);
            }
        }
    }
}
