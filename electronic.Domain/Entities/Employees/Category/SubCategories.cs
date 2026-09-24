using electronic.Domain.Abstractions;

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

    }
}
