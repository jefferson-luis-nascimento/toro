using System;
using TrendContext.Shared.Entities;

namespace TrendContext.Domain.Entities
{
    public class Order : Entity
    {
        public Guid UserId { get; set; }
        public Guid TrendId { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public virtual User User { get; set; }
        public virtual Trend Trend { get; set; }

        public static decimal CalculateTotalOrder(decimal currentPrice, decimal amount)
        {
            return Math.Round(currentPrice * amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
