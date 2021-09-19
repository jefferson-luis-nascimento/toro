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
using TrendContext.Domain.Repositories.Interfaces;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Handlers
{
    public class GetByIdUserPositionHandler : IRequestHandler<GetByIdUserPositionRequest, CommandResponse<GetByIdUserPositionResponse>>
    {
        private readonly IUserRepository userRepository;
        private readonly IOrderRepository orderRepository;

        private readonly ILogger<GetByIdUserPositionHandler> logger;

        public GetByIdUserPositionHandler(IUserRepository userRepository,
            IOrderRepository orderRepository,
            ILogger<GetByIdUserPositionHandler> logger)
        {
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.logger = logger;
        }

        public async Task<CommandResponse<GetByIdUserPositionResponse>> Handle(GetByIdUserPositionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await userRepository.GetByIdAsync(request.Id);

                if (result == null)
                {
                    return new CommandResponse<GetByIdUserPositionResponse>(false, 404, "User not found.", null);
                }

                var orders = await orderRepository.GetByUserIdAsync(request.Id);

                return new CommandResponse<GetByIdUserPositionResponse>(true, 200, string.Empty,
                    new GetByIdUserPositionResponse
                    {
                        Id = result.Id,
                        Name = result.Name,
                        CPF = result.CPF,
                        CheckingAccountAmount = result.CheckingAccountAmount,
                        Positions = orders?.Select(order => new CreateOrderResponse
                        {
                            Id = order.Id,  
                            Symbol = order.Trend.Symbol,
                            Amount = order.Amount,
                            CurrentPrice = order.Trend.CurrentPrice,
                            Total = order.Total,
                            OrderDate = order.CreatedIn,

                        }).ToList(),
                        Consolidated = result.CheckingAccountAmount + (orders.Sum(x => x.Total )),
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new CommandResponse<GetByIdUserPositionResponse>(false, 500, "Internal Server Error", null);
            }

        }
    }
}
