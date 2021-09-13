﻿using Newtonsoft.Json;

namespace TrendContext.Domain.Commands.Responses
{
    public class GetAllTrendResponse
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("currentPrice")]
        public decimal CurrentPrice { get; set; }
    }
}
