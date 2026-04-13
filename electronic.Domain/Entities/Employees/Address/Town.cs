using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees.Address
{
    public class Town : Entity
    {
        public Town()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }
    }
}
