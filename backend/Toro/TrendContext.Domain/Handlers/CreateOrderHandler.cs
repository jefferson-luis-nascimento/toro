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
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Handlers
{
    public class CreateOrderHandler : Notifiable<Notification>, IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly ITrendRepository trendRepository;
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrderHandler(IRepository<Order> orderRepository,
            ITrendRepository trendRepository,
            IRepository<User> userRepository,
            IUnitOfWork unitOfWork)
        {
            this.orderRepository = orderRepository;
            this.trendRepository = trendRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();

                if(!request.IsValid)
                {
                    AddNotifications(request);
                    return null;
                }

                var existingTrend = await trendRepository.GetBySymbol(request.Symbol);

                if (existingTrend == null)
                {
                    AddNotification("Symbol", "Symbol not found.");
                }

                var existingUser = await userRepository.GetByIdAsync(request.UserId);

                if(existingUser == null)
                {
                    AddNotification("UserId", "User not found.");
                }

                if(existingTrend.CurrentPrice * request.Amount <= existingUser.CheckingAccountAmount)
                {
                    AddNotification("Amount", "Insufficient funds.");
                }

                AddNotifications(request);

                if (!IsValid)
                {
                    return null;
                }

                var order = new Order
                {
                    TrendId = existingTrend.Id,
                    UserId = request.UserId,
                    Amount = request.Amount,
                };

                orderRepository.Create(order);
                unitOfWork.Commit();

                return new CreateOrderResponse
                {
                    Id = order.Id,
                    Symbol = existingTrend.Symbol,
                    Amount = order.Amount,
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
