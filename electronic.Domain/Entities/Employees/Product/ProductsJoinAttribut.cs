using electronic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees.Product
{
    public class ProductsJoinAttribut : Entity
    {
        public ProductsJoinAttribut()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public Guid AttributId { get; set; }
        public Attribut Attribut { get; set; }
    }
}
