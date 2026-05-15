using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace electronik.Domain.Entities.Users
{
    public class UserRole : IdentityUserRole<Guid>
    {
        [NotMapped]
        public virtual UserApp UserApp { get; set; }
        [NotMapped]
        public virtual RoleApp RoleApp { get; set; }
    }
}
