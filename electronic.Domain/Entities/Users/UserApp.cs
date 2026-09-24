using electronic.Domain.Entities.Employees;
using electronic.Domain.Entities.Employees.Addresses;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace electronik.Domain.Entities.Users
{
    public class UserApp: IdentityUser<Guid>
    {
        public UserApp()
        {
            Id = Guid.CreateVersion7();
            IsActive = true;
            IsDeleted = false;
            CreateAt = DateTimeOffset.Now;
        }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Salt { get; set; }
        public ICollection<Addresses>? Addresses { get; set; }
        [NotMapped]
        public ICollection<UserRole>? UserRole { get; set; }
        public ICollection<UserJoinOrder>? UserJoinOrder { get; set; }

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
