using electronik.Domain.Entities.Users;

namespace electronic.Application.Abstracts
{
    public interface IAuthTokenProcessor
    {
        (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(UserApp user);
        string GenerateRefreshToken();
        void WriteAuthTokenAsHttpOnlyCookie(string cookieName,string token, DateTime expiration);
    }
}
