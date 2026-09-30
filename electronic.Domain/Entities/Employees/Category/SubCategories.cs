using electronic.Domain.Abstractions;
using electronic.Domain.Entities.Employees.Product;

namespace electronic.Domain.Entities.Employees.Category
{
    public class SubCategories : Entity
    {
        public SubCategories()
        {
            Id = Guid.CreateVersion7();
        }

        public Categories Categories { get; set; }
        public Guid CategoriesId { get; set; }
        public ICollection<Products> Products { get; set; }


    }
}
