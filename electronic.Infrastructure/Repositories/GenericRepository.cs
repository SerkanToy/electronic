using electronic.Application.Interface;
using electronic.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace electronic.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly CilingirogluDbContext _context;
        private readonly DbSet<TEntity> dbSet;
        public GenericRepository(CilingirogluDbContext _context)
        {
            this._context = _context;
            this.dbSet = _context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(Guid Id)
        {
            var entity = dbSet.Find(Id)!;
            dbSet.Update(entity);
        } 

        public async Task<TEntity> GetByIdAsync(Guid Id)
        {
            return (await dbSet.FindAsync(Id))!;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return dbSet.AsQueryable();
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                // Log the exception or handle it as needed
                //throw new Exception("An error occurred while saving changes to the database.", ex);
                return 0;
            }
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
