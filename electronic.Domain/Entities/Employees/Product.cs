using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Product : Entity
    {
        public Product()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? icon { get; set; }
        public decimal RegulerPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string? Note { get; set; }
        public ICollection<Galleri> Galleries { get; set; }
        public ICollection<ProductJoinCategori> ProductJoinCategori { get; set; }
        public ICollection<ProductsJoinTags> ProductsJoinTags { get; set; }
        public ICollection<ProductJoinAttribut> ProductJoinAttributs { get; set; }
        public ICollection<ProductJoinCoupon> ProductJoinCoupons { get; set; }
    }
}
