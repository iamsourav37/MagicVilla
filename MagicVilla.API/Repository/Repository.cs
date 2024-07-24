
using MagicVilla.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace MagicVilla.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VillaDBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(VillaDBContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this._dbSet.AddAsync(entity);
            await this.SaveAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            this._dbSet.Remove(entity);
            await this.SaveAsync();
            return true;
        }

        public async Task SaveAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
