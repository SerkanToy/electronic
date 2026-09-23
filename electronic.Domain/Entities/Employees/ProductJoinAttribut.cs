using electronic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees
{
    public class ProductJoinAttribut : Entity
    {
        public ProductJoinAttribut()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid AttributId { get; set; }
        public Attribut Attribut { get; set; }
    }
}
