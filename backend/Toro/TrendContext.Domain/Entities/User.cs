using System.Collections.Generic;
using TrendContext.Shared.Entities;

namespace TrendContext.Domain.Entities
{
    public class User : Entity
    {
        public decimal CheckingAccountAmount { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public virtual List<Trend> Trends { get; set; }
    }
}
