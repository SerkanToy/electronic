using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees
{
    public class ProductJoinCategori
    {
        public ProductJoinCategori()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid Categori { get; set; }
        public Categori CategoriId { get; set; }
    }
}
