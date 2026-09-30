using electronic.Application.Interfaces;
using electronic.Application.UoW;
using electronic.Infrastructure.Context;
using electronic.Infrastructure.Repositories;

namespace electronic.Infrastructure.UoW
{
    public class UnitOfWork: IUnitOfWork
    {
        private CilingirogluDbContext cilingirogluDbContext;
        public UnitOfWork(CilingirogluDbContext cilingirogluDbContext)
        {
            this.cilingirogluDbContext = cilingirogluDbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {
            return new GenericRepository<TEntity>(cilingirogluDbContext);
        }

        public int SaveChanges()
        {
            return cilingirogluDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await cilingirogluDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            cilingirogluDbContext.Dispose();
        }

    }
}
