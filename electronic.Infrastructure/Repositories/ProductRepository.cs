using electronic.Application.Interface;
using electronic.Domain.Entities.Employees;

namespace electronic.Infrastructure.Repositories
{
    public class ProductRepository : IGenericRepository<Product>, IProductRepository
    {
        public Product Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
