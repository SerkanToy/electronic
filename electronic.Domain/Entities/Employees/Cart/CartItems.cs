using electronic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees.Cart
{
    public class CartItems : Entity
    {
        public CartItems()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
