using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Application.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void SaveChanges();
    }
}
