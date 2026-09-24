using electronic.Domain.Abstractions;
using electronic.Domain.Entities.Employees.Product;

namespace electronic.Domain.Entities.Employees
{
    public class Coupon: Entity
    {
        public Coupon()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public DateOnly StartDateOnly { get; set; }
        public TimeOnly StartTimeOnly { get; set; }
        public DateOnly EndDateOnly { get; set; }
        public TimeOnly EndTimeOnly { get; set; }
        public ICollection<ProductsJoinCoupon> ProductsJoinCoupons { get; set; }
    }
}
