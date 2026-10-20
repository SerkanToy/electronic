using electronic.Domain.DTOs.Products;

namespace electronic.Domain.DTOs.SubCategoryDtos
{
    public class SubCategoryDTOs
    {
        public string Id {  get; set; }
        public string Name {  get; set; }
        public string categoryName { get; set; }
        public IEnumerable<ProductsDto> productsDtos { get; set; }
    }
}
