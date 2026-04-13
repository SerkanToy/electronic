using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees
{
    public class ProductJoinCoupon
    {
        public ProductJoinCoupon()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CouponId { get; set; }
        public Coupon Coupon { get; set; }
    }
}
