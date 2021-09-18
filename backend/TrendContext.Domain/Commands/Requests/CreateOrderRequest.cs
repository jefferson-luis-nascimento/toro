using Flunt.Extensions.Br.Validations;
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
        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(CPF, "CPF", "CPF is required.")
                .IsCpf(CPF, "CPF", "CPF invalid.")
                .IsNotNullOrEmpty(Symbol, "Symbol", "Symbol is required.")
                .IsGreaterThan(Amount, 0, "Amount", "Amount is invalid")
                .IsFalse(Symbol.Length < 5 || Symbol.Length > 8, Symbol, "Symbol is invalid.")
            );
        }
    }
}
