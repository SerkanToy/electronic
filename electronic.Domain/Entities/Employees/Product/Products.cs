using electronic.Domain.Abstractions;
using electronic.Domain.Entities.Employees.Category;
using System.ComponentModel.DataAnnotations;

namespace electronic.Domain.Entities.Employees.Product
{
    public class Products : Entity
    {
        public Products()
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
        public ICollection<ProductImages> ProductImages { get; set; }
        public Categories Categories { get; set; }
        public Guid CategoriesId { get; set; }
        public SubCategories SubCategories { get; set; }
        public Guid SubCategoriesId { get; set; }
        public Brands Brands { get; set; }
        public Guid BrandsId { get; set; }
        public ICollection<ProductsJoinTags> ProductsJoinTags { get; set; }
        public ICollection<ProductsJoinAttribut> ProductsJoinAttributs { get; set; }
        public ICollection<ProductsJoinCoupon> ProductsJoinCoupons { get; set; }
        public ICollection<ProductReviews> ProductReviews { get; set; }
    }
}
