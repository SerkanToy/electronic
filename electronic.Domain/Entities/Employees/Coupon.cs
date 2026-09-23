using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Coupon: Entity
    {
        public Coupon()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public DateOnly StartTateOnly { get; set; }
        public TimeOnly StartTimeOnly { get; set; }
        public DateOnly EndTateOnly { get; set; }
        public TimeOnly EndTimeOnly { get; set; }
        public ICollection<ProductJoinCoupon> ProductJoinCoupons { get; set; }
    }
}
