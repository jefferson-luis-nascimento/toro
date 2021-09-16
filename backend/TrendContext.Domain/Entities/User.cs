using System.Collections.Generic;
using TrendContext.Shared.Entities;

namespace TrendContext.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public decimal CheckingAccountAmount { get; set; }
    }
}
