using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly KahootContext _dbcontext;

        public GenericRepository(KahootContext context)
        {
            _dbSet = context.Set<TEntity>();
            _dbcontext = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeletedAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>?> GetAsync(Expression<Func<TEntity, bool>>? condition = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }
    }
}
