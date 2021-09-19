using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendContext.Domain.Entities;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetByUserIdAsync(Guid id);
    }
}
