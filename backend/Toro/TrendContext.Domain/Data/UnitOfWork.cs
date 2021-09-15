using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrendContext.Domain.Data.Interfaces;

namespace TrendContext.Domain.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InMemoryAppContext appContext;

        public UnitOfWork(InMemoryAppContext appContext)
        {
            this.appContext = appContext;
        }

        public void BeginTransaction()
        {
        }

        public async Task Commit()
        {
            await appContext.SaveChangesAsync();
        }

        public void Rollback()
        {
        }
    }
}
