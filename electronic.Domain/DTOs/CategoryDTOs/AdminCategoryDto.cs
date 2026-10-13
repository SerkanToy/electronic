using electronic.Domain.Entities.Employees.Category;
using electronic.Domain.Entities.Employees.Product;

namespace electronic.Domain.DTOs.CategoryDTOs
{
    public class AdminCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string icon { get; set; }
        public IEnumerable<Products> Products { get; set; }
        public IEnumerable<SubCategories> SubCategories { get; set; }
    }
}
