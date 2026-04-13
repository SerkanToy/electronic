using Microsoft.AspNetCore.Identity;

namespace electronik.Domain.Entities.Users
{
    public class RoleApp: IdentityRole<Guid>
    {
        public RoleApp()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
