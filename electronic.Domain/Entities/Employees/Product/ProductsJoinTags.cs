using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees.Product
{
    public class ProductsJoinTags
    {
        public ProductsJoinTags()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public Guid TagId { get; set; }
        public Tags Tags { get; set; }
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
    }
}
