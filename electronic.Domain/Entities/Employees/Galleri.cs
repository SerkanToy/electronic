using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Galleri:Entity
    {
        public Galleri()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
