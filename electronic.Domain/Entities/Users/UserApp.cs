using Microsoft.AspNetCore.Identity;

namespace electronik.Domain.Entities.Users
{
    public class UserApp: IdentityUser<string>
    {
        public UserApp()
        {
            Id = Guid.NewGuid().ToString();
            IsActive = true;
            IsDeleted = false;
        }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string SicilNo { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        //public ICollection<TimeUserCreate>? TimeUserCreates { get; set; }
        //public ICollection<DateUserCreate>? DateUserCreates { get; set; }
    }
}
