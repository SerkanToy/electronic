using electronic.Domain.Entities.Employees;
using System.Security.AccessControl;

namespace electronic.Domain.Dtos.Categoris
{
    public class CategoriDTO
    {
        public CategoriDTO()
        {
            Products = new List<Product>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? icon { get; set; }
        public List<Product>? Products { get; set; }
        public string CreateBy { get; set; }
    }
}
