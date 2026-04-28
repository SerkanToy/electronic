using electronik.Domain.Entities.Users;

namespace electronic.Application.Abstracts
{
    public interface ITokenService
    {
        string CreateToken(UserApp user, IEnumerable<string> roles);
    }
}
