using electronic.Domain.Abstractions;
using electronic.Domain.Entities.Employees.Product;

namespace electronic.Domain.Entities.Employees.Category
{
    public class Categories : Entity
    {
        public Categories()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? icon { get; set; }
        public ICollection<Products> Products { get; set; }
        public ICollection<SubCategories> SubCategories { get; set; } 
    }
}
