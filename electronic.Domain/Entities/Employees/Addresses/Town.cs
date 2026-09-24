using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees.Addresses
{
    public class Town : Entity
    {
        public Town()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Addresses> Address { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
