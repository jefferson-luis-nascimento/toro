using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrendContext.Domain.Data;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repository.Interfaces;

namespace TrendContext.Domain.Repository.Implementations
{
    public class TrendRepository : Repository<Trend>, ITrendRepository
    {
        public TrendRepository(InMemoryAppContext appContext) 
            : base (appContext)
        {

        }

        public async Task<Trend> GetBySymbol(string symbol)
        {
            return await entities.FirstOrDefaultAsync(trend => trend.Symbol == symbol);
        }
    }
}
