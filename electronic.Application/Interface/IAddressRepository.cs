using electronic.Domain.Entities.Employees;
using electronic.Domain.Entities.Employees.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Application.Interface
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
    }
}
