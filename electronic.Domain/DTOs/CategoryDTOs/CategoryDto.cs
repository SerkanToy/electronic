using electronic.Domain.DTOs.SubCategoryDtos;

namespace electronic.Domain.DTOs.CategoryDTOs
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubCategoryDTOs> SubCategoryDTOs { get; set; }
    }
}
