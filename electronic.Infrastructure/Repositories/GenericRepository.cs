using electronic.Application.Interfaces;
using electronic.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace electronic.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CilingirogluDbContext context;
        protected readonly DbSet<T> dbSet;
        public GenericRepository(CilingirogluDbContext context) 
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
        {
            var query = dbSet.Where(predicate);
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = true)
        {
            var query = asNoTracking ? dbSet.AsNoTracking() : dbSet;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithInclude(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression, bool asNoTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeExpression != null)
            {
                query = includeExpression(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
        {
            var query = dbSet.AsQueryable();
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> GetByIdAsyncExpressionWithInclude(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression, bool asNoTracking = true)
        {
            var query = dbSet.Where(predicate);
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            query = includeExpression(query);
            return await query.FirstOrDefaultAsync();
        }

        public async Task Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
