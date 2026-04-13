using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Categori: Entity
    {
        public Categori()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? icon { get; set; }
        public ICollection<ProductJoinCategori> ProductJoinCategori { get; set; }
    }
}
