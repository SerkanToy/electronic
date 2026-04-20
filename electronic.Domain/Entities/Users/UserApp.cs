using electronic.Domain.Entities.Employees.Address;
using Microsoft.AspNetCore.Identity;

namespace electronik.Domain.Entities.Users
{
    public class UserApp: IdentityUser<Guid>
    {
        public UserApp()
        {
            Id = Guid.CreateVersion7();
            IsActive = true;
            IsDeleted = false;
        }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Salt { get; set; }
        public ICollection<Address>? Addresses { get; set; }

        #region Audit Log
            public DateTimeOffset CreateAt { get; set; }
            public Guid CreateUserId { get; set; } = default!;
            public DateTimeOffset? UpdateAt { get; set; }
            public Guid? UpdateUserId { get; set; }
            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
            public DateTimeOffset? DeleteAt { get; set; }
            public Guid? DeleteUserId { get; set; }
        #endregion
    }
}
