using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace electronic.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate = null, bool asNoTracking = true);
        Task<T?> GetByIdAsyncExpressionWithInclude(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression = null, bool asNoTracking = true);
        Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = true, Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetAllAsyncWithInclude(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression = null, bool asNoTracking = true);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null, bool asNoTracking = true);
        Task AddAsync(T entity);
        Task AddAsync(List<T> entity);
        Task Update(T entity);
    }
}
