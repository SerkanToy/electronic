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
            (var user, bool userExists) = await findUser(dto.Email);
            if (!userExists)
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
                Email = user.Email,
                Token = tokenService.CreateToken(user, roles.ToList())
            };

            return responseModel;
        }

        public async Task<ResponseModel> Register(UserApp user, string passwordHash)
        {
            //var user = await userManager.FindByEmailAsync(dto.Email);
            (UserApp userData, bool userExists) = await findUser(user.Email);
            if(userExists)
            {
                responseModel.Data = user;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Kullanıcı zaten mevcut" };
                return responseModel;
            }

            var result = await userManager.CreateAsync(user, passwordHash);

            if(!result.Succeeded)
            {
                responseModel.Data = user;
                responseModel.IsSuccess = false;
                responseModel.Message = result.Errors.Select(e => e.Description).ToList();
                return responseModel;
            }

            await userManager.AddToRoleAsync(user, "NormalUser");
            var roles = await userManager.GetRolesAsync(user);
            responseModel.IsSuccess = true;
            responseModel.Message = new List<string> { "Kayıt işlemi başarılı" };
            responseModel.Data = new RegisterDTO
            {
                FirstName = user.Name,
                LastName = user.SurName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
            };

            return responseModel;
        }

        private async Task<(UserApp, bool)> findUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            bool userExists = user != null ? true : false;
            return await Task.FromResult((user, userExists));
        }
    }
}
