using Newtonsoft.Json;
using System;

namespace TrendContext.Domain.Commands.Responses
{
    public class GetAllUsersResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("checkingAccountAmount")]
        public decimal CheckingAccountAmount { get; set; }
    }
}
