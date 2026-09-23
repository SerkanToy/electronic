using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class ProductJoinCoupon: Entity
    {
        public ProductJoinCoupon()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CouponId { get; set; }
        public Coupon Coupon { get; set; }
    }
}
