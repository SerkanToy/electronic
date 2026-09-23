using electronik.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Employees
{
    public class UserJoinOrder
    {        
        public Guid UserId { get; set; }
        public UserApp UserApp { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
