using electronic.Domain.Dtos.ProductDto;
using electronic.Domain.Entities.Employees;

namespace electronic.Domain.Dtos.CategoriDto
{
    public class CategoriListDTO
    {
        public CategoriListDTO()
        {
            ProductListDto = new List<ProductListDto>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? icon { get; set; }
        public List<ProductListDto>? ProductListDto { get; set; }
        public string CreateBy { get; set; }
    }
}
