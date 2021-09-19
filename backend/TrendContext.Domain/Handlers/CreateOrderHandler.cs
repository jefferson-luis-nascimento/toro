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
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Handlers
{
    public class CreateOrderHandler : Notifiable<Notification>, IRequestHandler<CreateOrderRequest, CommandResponse<CreateOrderResponse>>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly ITrendRepository trendRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CreateOrderHandler> logger;

        public CreateOrderHandler(IRepository<Order> orderRepository,
            ITrendRepository trendRepository,
            IUserRepository userRepository,
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
                    return new CommandResponse<CreateOrderResponse>(false, 400, string.Join(Environment.NewLine, request.Notifications.Select(x => x.Message)), null);
                }

                var existingTrend = await trendRepository.GetBySymbol(request.Symbol);

                if (existingTrend == null)
                {
                    return new CommandResponse<CreateOrderResponse>(false, 404, "Trend not found.", null);
                }

                var existingUser = await userRepository.GetByCPFAsync(request.CPF);

                if (existingUser == null)
                {
                    return new CommandResponse<CreateOrderResponse>(false, 404, "User not found.", null);
                }

                var order = new Order
                {
                    TrendId = existingTrend.Id,
                    UserId = existingUser.Id,
                    Amount = request.Amount,
                    Total = Order.CalculateTotalOrder(existingTrend.CurrentPrice , request.Amount),
                };

                if (order.Total > existingUser.CheckingAccountAmount)
                {
                    return new CommandResponse<CreateOrderResponse>(false, 400, "Insufficient funds.", null);
                }

                unitOfWork.BeginTransaction();

                orderRepository.Create(order);

                existingUser.CheckingAccountAmount -= order.Total;
                await userRepository.UpdateAsync(existingUser);

                await unitOfWork.Commit();

                return new CommandResponse<CreateOrderResponse>(true, 201, string.Empty,
                    new CreateOrderResponse
                    {
                        Id = order.Id,
                        Symbol = existingTrend.Symbol,
                        Amount = order.Amount,
                        CurrentPrice = existingTrend.CurrentPrice,
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
