using electronic.Domain.DTOs.CategoryDTOs;
using electronic.Domain.Entities.Employees.Category;

namespace electronic.Domain.DTOs.Products
{
    public class ProductsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string categoryName {  get; set; }
        public string subCategoryName {  get; set; }
    }
}
