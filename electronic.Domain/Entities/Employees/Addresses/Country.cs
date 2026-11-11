using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees.Addresses
{
    public class Country : Entity
    {
        public Country()
        {
            Id = Guid.CreateVersion7();
        }
        public string Name { get; set; }
        public ICollection<City>? City { get; set; }
        public ICollection<Addresses>? Address { get; set; }
    }
}
