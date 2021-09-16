using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendContext.Shared.Entities;

namespace TrendContext.Shared.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void Create(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task Save();
    }
}
