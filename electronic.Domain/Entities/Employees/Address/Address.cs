using electronic.Domain.Abstractions;
using electronik.Domain.Entities.Users;

namespace electronic.Domain.Entities.Employees.Address;


public class Address : Entity
{
    public Address()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FulLAddress { get; set; }
    public Guid CountryId { get; set; }
    public Country Country { get; set; }
    public Guid CityId { get; set; }
    public City City { get; set; }
    public Guid TownId { get; set; }
    public Town Town { get; set; }
    public Guid UserAppId { get; set; }
    public UserApp UserApp { get; set; }
    
}
