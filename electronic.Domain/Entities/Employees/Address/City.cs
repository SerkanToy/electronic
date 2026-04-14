using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees.Address
{
    public class City : Entity
    {
        public City()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Town> Town { get; set; }
    }
}
