using electronic.Application.IService;

namespace electronic.Infrastructure.Service
{
    public class TokenService : ITokenService
    {
        public string CreateToken(object user, List<string> roles)
        {
            return "token";
        }
    }
}
