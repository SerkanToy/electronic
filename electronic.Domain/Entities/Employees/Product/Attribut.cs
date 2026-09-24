using electronic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees.Product
{
    public class Attribut : Entity
    {
        public Attribut()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public ICollection<ProductsJoinAttribut> ProductsJoinAttributs { get; set; }    
    }
}
