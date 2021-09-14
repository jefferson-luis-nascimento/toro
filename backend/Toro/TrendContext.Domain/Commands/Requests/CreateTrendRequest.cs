using MediatR;
using Newtonsoft.Json;
using TrendContext.Domain.Commands.Responses;

namespace TrendContext.Domain.Commands.Requests
{
    public class CreateTrendRequest : IRequest<CreateTrendResponse>
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("currentPrice")]
        public decimal CurrentPrice { get; set; }
    }
}
