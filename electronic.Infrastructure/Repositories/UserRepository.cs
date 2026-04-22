using electronic.Application.Interface;
using electronic.Infrastructure.Context;
using electronik.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace electronic.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CilingirogluDbContext cilingirogluDbContext;
        public UserRepository(CilingirogluDbContext cilingirogluDbContext)
        {
            this.cilingirogluDbContext = cilingirogluDbContext;
        }

        public Task<UserApp>? GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = cilingirogluDbContext.UserApp.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            return user;
        }
    }
}
