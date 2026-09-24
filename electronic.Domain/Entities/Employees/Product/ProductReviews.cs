using electronic.Domain.Abstractions;
using electronik.Domain.Entities.Users;

namespace electronic.Domain.Entities.Employees.Product
{
    public class ProductReviews : Entity
    {
        public ProductReviews()
        {
            Id = Guid.CreateVersion7();
        }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateOnly StartDateOnly { get; set; }
        public TimeOnly StartTimeOnly { get; set; }
        public Guid ProductsId { get; set; }
        public Products Products { get; set; }
    }
}
