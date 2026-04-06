using Microsoft.AspNetCore.Identity;

namespace electronik.Domain.Entities.Users
{
    public class RoleApp: IdentityRole<string>
    {
        public RoleApp()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
