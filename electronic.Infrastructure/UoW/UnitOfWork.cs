using electronic.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
