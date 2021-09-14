using Newtonsoft.Json;
using System;

namespace TrendContext.Domain.Commands.Responses
{
    public class CreateTrendResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("currentPrice")]
        public decimal CurrentPrice { get; set; }
    }
}
