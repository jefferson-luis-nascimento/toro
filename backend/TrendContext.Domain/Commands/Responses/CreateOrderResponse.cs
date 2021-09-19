using Newtonsoft.Json;
using System;

namespace TrendContext.Domain.Commands.Responses
{
    public class CreateOrderResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currentPrice")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }
    }
}
