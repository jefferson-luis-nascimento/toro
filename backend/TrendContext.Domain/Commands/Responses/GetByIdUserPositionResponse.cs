using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TrendContext.Domain.Commands.Responses
{
    public class GetByIdUserPositionResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("checkingAccountAmount")]
        public decimal CheckingAccountAmount { get; set; }

        [JsonProperty("positions")]
        public List<CreateOrderResponse> Positions { get; set; }

        [JsonProperty("consolidated")]
        public decimal Consolidated { get; set; }

    }
}
