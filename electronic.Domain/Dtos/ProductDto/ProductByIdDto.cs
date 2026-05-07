using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Dtos.ProductDto
{
    public class ProductByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal RegulerPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Note { get; set; }
        public string? Icon { get; set; }
    }
}
