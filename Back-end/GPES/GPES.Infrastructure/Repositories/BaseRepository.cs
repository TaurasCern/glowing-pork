using GPES.Domain.Models.Entities;
using GPES.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GPES.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly GPESContext _context;  
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(GPESContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<TEntity> GetById(Guid id)
        {
            // TODO: Think of a less scuffed way to do this
            return await _dbSet.FirstOrDefaultAsync(x => ((IBaseDatabaseEntity)x).Id == id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
