using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Application.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid Id);
        Task CreateAsync(TEntity entity);
        void Delete(Guid Id);
        void Update(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
