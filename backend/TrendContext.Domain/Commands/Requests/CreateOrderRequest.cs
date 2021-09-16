using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Newtonsoft.Json;
using System;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Shared.Commands;

namespace TrendContext.Domain.Commands.Requests
{
    public class CreateOrderRequest : Notifiable<Notification>, IRequest<CommandResponse<CreateOrderResponse>>, ICommand
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsFalse(UserId == default, "CreateTrendRequest.UserId", "UserId is required.")
                .IsNotNullOrEmpty(Symbol, "CreateTrendRequest.Symbol", "Symbol is required.")
                .IsGreaterThan(Amount, 0, "CreateTrendRequest.Amount", "Amount is invalid")
                .IsFalse(Symbol.Length < 5 || Symbol.Length > 8, Symbol, "Symbol is invalid.")
            );
        }
    }
}
