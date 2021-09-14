﻿using System.Threading.Tasks;
using TrendContext.Domain.Entities;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Repository.Interfaces
{
    public interface ITrendRepository : IRepository<Trend>
    {
        Task<Trend> GetBySymbol(string symbol);
    }
}
