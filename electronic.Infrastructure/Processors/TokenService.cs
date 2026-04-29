using electronic.Application.Abstracts;
using electronic.Infrastructure.Options;
using electronik.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace electronic.Infrastructure.Processors
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtOptions jwtOptions;
        public TokenService(IOptions<JwtOptions> jwtOptions, IConfiguration configuration)
        {
            _configuration = configuration;
            this.jwtOptions = jwtOptions.Value;
        }
        public string CreateToken(UserApp user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim("FullName",$"{user.Name} {user.SurName}"),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:Secret"]!));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10.0),
                SigningCredentials = credentials,
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
            };

            var jwtToken = new JwtSecurityTokenHandler();
            var token = jwtToken.CreateToken(tokenDescriptor);

            return jwtToken.WriteToken(token);
        }
    }
}
