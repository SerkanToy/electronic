using electronic.Application.Abstracts;
using electronic.Infrastructure.Options;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace electronic.Infrastructure.Processors
{
    public class AuthTokenProcessor : IAuthTokenProcessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly JwtOptions jwtOptions;

        public AuthTokenProcessor(IOptions<JwtOptions> jwtOptions, IHttpContextAccessor httpContextAccessor)
        {
            this.jwtOptions = jwtOptions.Value;
            this.httpContextAccessor = httpContextAccessor;
        }
        public (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(UserApp user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("FullName",$"{user.Name} {user.SurName}"),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim("RefreshToken",user.RefreshToken),
                new Claim("RefreshTokenExpiresAtUtc",user.RefreshTokenExpiresAtUtc.ToString()),
            };

            var expires = DateTime.UtcNow.AddMinutes(jwtOptions.ExpirationTimeInMinutes);

            var token = new JwtSecurityToken(
                    issuer: jwtOptions.Issuer,
                    audience: jwtOptions.Audience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token); 

            return (jwtToken, expires);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName,token,new CookieOptions
            {
                HttpOnly = true,
                Expires = expiration,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
