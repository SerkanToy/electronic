using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace electronik.Domain.Entities.Users
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public override Guid UserId { get; set; }
        [NotMapped]
        public UserApp? UserApp { get; set; } 
        public override Guid RoleId { get; set; }
        [NotMapped] 
        public RoleApp? RoleApp { get; set; }
    }
}
