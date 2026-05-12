using electronic.Application.Interface;
using electronic.Domain.Entities.Employees.Address;
using electronic.Infrastructure.Context;

namespace electronic.Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(CilingirogluDbContext _context) : base(_context)
        {
        }
    }
}
