using electronic.Domain.Abstractions;
using electronic.Domain.Entities.Employees.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees
{
    public class Brands : Entity
    {
        public Brands()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
