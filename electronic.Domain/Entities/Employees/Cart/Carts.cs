using electronic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees.Cart
{
    public class Carts : Entity
    {
        public Carts()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
