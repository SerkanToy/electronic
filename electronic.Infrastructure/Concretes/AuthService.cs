using electronic.Application.Abstracts;
using electronic.Domain.Dtos.Login;
using electronic.Domain.Dtos.UserDtos;
using electronic.Domain.Models;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace electronic.Infrastructure.Concretes
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<UserApp> userManager;
        private readonly SignInManager<UserApp> signInManager;
        private readonly ITokenService tokenService;
        private readonly ResponseModel<UserDTO> responseModel;
        public AuthService(UserManager<UserApp> userManager, 
                        SignInManager<UserApp> signInManager, 
                        ITokenService tokenService, 
                        ResponseModel<UserDTO> responseModel)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.responseModel = responseModel;
        }

        public async Task<ResponseModel<UserDTO>> Login(LoginDTO dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if(user is null)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Geçersiz Email veya Şifre." };
                return responseModel;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            // var result = await userManager.CheckPasswordAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Geçersiz Email veya Şifre." };
                return responseModel;
            }

            var roles = await userManager.GetRolesAsync(user);
            responseModel.IsSuccess = true;
            responseModel.Data = new UserDTO
            {
                Id = user.Id,
                Email = user.Email!,
                FullName = $"{user.Name} {user.SurName}",
                Token = tokenService.CreateToken(user, roles)
            };
            return responseModel;
        }

        public async Task<ResponseModel<UserDTO>> Register(RegisterDTO dto)
        {
            var user = new UserApp
            {
                Name = dto.FirstName,
                SurName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email.Split('@')[0],
            };

            var result = await userManager.CreateAsync(user, dto.Password);
            if(!result.Succeeded)
            {
                responseModel.Message = new List<string>();
                responseModel.IsSuccess = false;
                result.Errors.ToList().ForEach(e => responseModel.Message.Add(e.Description));
                return responseModel;
            }

            await userManager.AddToRoleAsync(user, "NormalUser");
            var roles = await userManager.GetRolesAsync(user);
            responseModel.IsSuccess = true;
            responseModel.Data = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = $"{user.Name} {user.SurName}",
                Token = tokenService.CreateToken(user, roles)
            };

            return responseModel;
        }
    }
}
