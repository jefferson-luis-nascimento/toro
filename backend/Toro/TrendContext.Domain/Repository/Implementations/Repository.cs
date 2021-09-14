using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendContext.Domain.Data;
using TrendContext.Shared.Entities;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Repository.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly InMemoryAppContext _appContext;
        protected DbSet<TEntity> entities;

        public Repository(InMemoryAppContext appContext)
        {
            _appContext = appContext;
            entities = _appContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var existsEntity = await entities.SingleOrDefaultAsync(entity => entity.Id == id);

            if (existsEntity == null)
            {
                throw new EntryPointNotFoundException("id");
            }

            return existsEntity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public void Create(TEntity entity)
        {
            entities.Add(entity);
        }        

        public async Task UpdateAsync(TEntity entity)
        {
            var existsEntity = await GetByIdAsync(entity.Id);

            existsEntity = entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var existsEntity = await GetByIdAsync(id);

            entities.Remove(existsEntity);
        }

        public async Task Save()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}
