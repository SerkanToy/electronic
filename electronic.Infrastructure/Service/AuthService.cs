using electronic.Application.IService;
using electronic.Domain.DTOs.AuthDTOs;
using electronic.Domain.DTOs.UserDTOs;
using electronic.Infrastructure.Models;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace electronic.Infrastructure.Service
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<UserApp> userManager;
        private readonly SignInManager<UserApp> signInManager;
        private ResponseModel responseModel;
        private ITokenService tokenService;
        
        public AuthService(UserManager<UserApp> userManager = null, 
                           SignInManager<UserApp> signInManager = null,
                           ITokenService tokenService = null,
                           ResponseModel responseModel = null)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.responseModel = responseModel;
        }

        public async Task<ResponseModel> Login(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if(user == null)
            {
                responseModel.Data = dto;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Kullanıcı bulunamadı" };
                return responseModel;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if(!result.Succeeded)
            {
                responseModel.Data = dto;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Giriş işlemi başarısız" };
                return responseModel;
            }

            var roles = await userManager.GetRolesAsync(user);

            responseModel.IsSuccess = true;
            responseModel.Message = new List<string> { "Giriş işlemi başarılı" };
            responseModel.Data = new UserDTO
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                Token = tokenService.CreateToken(user, roles.ToList())
            };

            return responseModel;
        }

        public async Task<ResponseModel> Register(RegisterDTO dto)
        {
            return responseModel;
        }
    }
}
