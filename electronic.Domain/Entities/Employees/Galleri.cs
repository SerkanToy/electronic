using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Galleri:Entity
    {
        public Galleri()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Orderby {  get; set; }
    }
}
