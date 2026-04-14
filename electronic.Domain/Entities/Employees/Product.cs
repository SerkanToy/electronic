using electronic.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Entities.Employees
{
    public class Product : Entity
    {
        public Product()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? icon { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal RegulerPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal DiscountPrice { get; set; }
        public string? Note { get; set; }
        public ICollection<Galleri> Galleries { get; set; }
        public ICollection<ProductJoinCategori> ProductJoinCategori { get; set; }
        public ICollection<ProductsJoinTags> ProductsJoinTags { get; set; }
        public ICollection<ProductJoinAttribut> ProductJoinAttributs { get; set; }
        public ICollection<ProductJoinCoupon> ProductJoinCoupons { get; set; }
    }
}
