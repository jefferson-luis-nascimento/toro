using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendContext.Domain.Data.Interfaces;

namespace TrendContext.Domain.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory<InMemoryAppContext> contextFactory;
        private readonly InMemoryAppContext context;

        public UnitOfWork(IDbContextFactory<InMemoryAppContext> contextFactory)
        {
            this.contextFactory = contextFactory;
            this.context = contextFactory.CreateDbContext();
        }

        public void Commit()
        {
            context.SaveChangesAsync();
        }

        public void Rollback()
        {
        }
    }
}
