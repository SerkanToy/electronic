namespace electronic.Domain.Entities.Employees
{
    public class Coupon
    {
        public Coupon()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public ICollection<ProductJoinCoupon> ProductJoinCoupons { get; set; }
    }
}
