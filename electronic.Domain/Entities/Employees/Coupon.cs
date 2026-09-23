using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Coupon: Entity
    {
        public Coupon()
        {
            Id = Guid.CreateVersion7();
        }
        public ICollection<ProductJoinCoupon> ProductJoinCoupons { get; set; }
    }
}
