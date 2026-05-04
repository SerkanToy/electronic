using electronic.Application.Interface;
using electronic.Domain.Entities.Employees;
using electronic.Infrastructure.Context;

namespace electronic.Infrastructure.Repositories
{
    public class CategoriRepository : GenericRepository<Categori>, ICategoriRepository
    {
        public CategoriRepository(CilingirogluDbContext _context) : base(_context)
        {
        }
    }
}
