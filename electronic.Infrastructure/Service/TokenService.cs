using electronic.Application.IService;
using electronik.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace electronic.Infrastructure.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateToken(UserApp user, List<string> roles)
        {
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new Claim("FullName", $"{user.Name} {user.SurName}"),
                new Claim(ClaimTypes.Name, user.UserName!)
            };


            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]!));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtOptions:ExpirationTimeInMinutes"])),                
                SigningCredentials = creds,
                Issuer = configuration["JwtOptions:Issuer"],
                Audience = configuration["JwtOptions:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
