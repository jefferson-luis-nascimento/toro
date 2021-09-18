using Newtonsoft.Json;
using System;

namespace TrendContext.Domain.Commands.Responses
{
    public class CreateSessionResponse : CreateUserResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
