using Microsoft.AspNetCore.Identity;

namespace electronik.Domain.Entities.Users
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public UserApp UserApp { get; set; }
        public RoleApp RoleApp { get; set; }
    }
}
