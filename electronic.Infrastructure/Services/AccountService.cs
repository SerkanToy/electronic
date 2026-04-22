using electronic.Application.Abstracts;
using electronic.Application.Interface;
using electronic.Domain.Exceptions;
using electronic.Domain.Requests;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace electronic.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthTokenProcessor authTokenProcessor;
        private readonly UserManager<UserApp> userManager;
        private readonly IUserRepository userRepository;

        public AccountService(IAuthTokenProcessor authTokenProcesso, 
            UserManager<UserApp> userManager,
            IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.authTokenProcessor = authTokenProcesso;
            this.userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterRequest registerRequest)
        {
            var userExists = await userManager.FindByEmailAsync(registerRequest.Email) != null;
            if (userExists)
                throw new UserAlreadyExistsException(email: registerRequest.Email);

            IdentityResult result = await userManager.CreateAsync(new UserApp
            {
                Email = registerRequest.Email,
                UserName = registerRequest.Email.Split("@")[0],
                Name = registerRequest.Name,
                SurName = registerRequest.SurName,
            },registerRequest.Password);

            if (!result.Succeeded) 
            {
                throw new RegistrationFailedException(result.Errors.Select(e => e.Description));
            }
        }

        public async Task LoginAsync(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            var passwordCheck = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (user == null || !passwordCheck)
                throw new LoginFailedException(loginRequest.Email);

            var (jwtToken, expirationDateInUtc) = authTokenProcessor.GenerateJwtToken(user);
            var refreshToken = authTokenProcessor.GenerateRefreshToken();

            var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

            await userManager.UpdateAsync(user);

            authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);

        }                

        public async Task RefreshTokenAsync(string? refreshToken)
        {            
            throw new NotImplementedException();
        }
    }
}
