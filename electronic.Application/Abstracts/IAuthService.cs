using electronic.Domain.Dtos.Login;
using electronic.Domain.Dtos.UserDtos;
using electronic.Domain.Models;

namespace electronic.Application.Abstracts
{
    public interface IAuthService
    {
        Task<ResponseModel<UserDTO>> Register(RegisterDTO dto);
        Task<ResponseModel<UserDTO>> Login(LoginDTO dto);
    }
}
