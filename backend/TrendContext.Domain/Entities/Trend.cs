using System.Collections.Generic;
using TrendContext.Shared.Entities;

namespace TrendContext.Domain.Entities
{
    public class Trend : Entity
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
