using Microsoft.AspNetCore.Identity;

namespace electronik.Domain.Entities.Users
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual Guid UserId { get; set; }
        
        public virtual UserApp UserApp { get; set; }
        public virtual Guid RoleId { get; set; }
        public virtual RoleApp RoleApp { get; set; }
    }
}
