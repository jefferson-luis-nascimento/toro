using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendContext.Domain.Data;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repositories.Interfaces;

namespace TrendContext.Domain.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(InMemoryAppContext appContext) 
            : base (appContext)
        {
        }

        public async Task<List<Order>> GetByUserIdAsync(Guid id)
        {
            return await entities.Include(x => x.Trend).Where(order => order.UserId == id).ToListAsync();
        }
    }
}
