using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class ProductJoinCategori: Entity
    {
        public ProductJoinCategori()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CategoriId { get; set; }
        public Categori Categori { get; set; }
    }
}
