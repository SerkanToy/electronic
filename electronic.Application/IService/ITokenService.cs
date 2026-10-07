using electronik.Domain.Entities.Users;

namespace electronic.Application.IService
{
    public interface ITokenService
    {
        string CreateToken(UserApp user, List<string> roles);
    }
}
