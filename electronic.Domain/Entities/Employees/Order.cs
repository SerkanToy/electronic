using electronic.Domain.Abstractions;

namespace electronic.Domain.Entities.Employees
{
    public class Order : Entity
    {
        public Order()
        {
            Id = Guid.CreateVersion7();
        }
        public string OrderNumber { get; set; } = string.Empty;
        public ICollection<UserJoinOrder>? UserJoinOrder { get; set; }
    }
}
