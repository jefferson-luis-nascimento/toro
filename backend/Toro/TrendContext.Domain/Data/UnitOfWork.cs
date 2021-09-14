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
        private readonly InMemoryAppContext context;

        public UnitOfWork(InMemoryAppContext context)
        {
            this.context = context;
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
