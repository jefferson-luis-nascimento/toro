using System.Collections.Generic;
using TrendContext.Shared.Entities;

namespace TrendContext.Domain.Entities
{
    public class Trend : Entity
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
