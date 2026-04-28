using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Dtos.UserDtos
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
