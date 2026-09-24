using electronic.Domain.Abstractions;
using electronic.Domain.Entities.Employees.Product;

namespace electronic.Domain.Entities.Employees
{
    public class ProductImages:Entity
    {
        public ProductImages()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public int Orderby {  get; set; }
    }
}
