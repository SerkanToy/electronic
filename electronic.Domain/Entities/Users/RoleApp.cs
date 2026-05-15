using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace electronik.Domain.Entities.Users
{
    public class RoleApp: IdentityRole<Guid>
    {
        public RoleApp()
        {
            Id = Guid.CreateVersion7();
        }
        [NotMapped]
        public ICollection<UserRole>? UserRole { get; set; }
    }
}
