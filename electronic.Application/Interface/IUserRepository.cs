using electronik.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Application.Interface
{
    public interface IUserRepository
    {
        Task<UserApp>? GetUserByRefreshTokenAsync(string refreshToken);
    }
}
