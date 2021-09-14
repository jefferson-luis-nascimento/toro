using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendContext.Domain.Entities;

namespace TrendContext.Domain.Data
{
    public class InMemoryAppContext : DbContext
    {
        public InMemoryAppContext(DbContextOptions<InMemoryAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trend> Trends { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
