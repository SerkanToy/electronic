using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Tags : Entity
    {
        public Tags()
        {
            Id = Guid.CreateVersion7();
        }
        public ICollection<ProductsJoinTags> ProductsJoinTags { get; set; }
    }
}
