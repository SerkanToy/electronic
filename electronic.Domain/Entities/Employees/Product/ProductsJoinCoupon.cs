using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees.Product
{
    public class ProductsJoinCoupon: Entity
    {
        public ProductsJoinCoupon()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public Guid CouponId { get; set; }
        public Coupon Coupon { get; set; }
    }
}
