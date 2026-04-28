using electronic.Domain.Dtos.Login;
using electronic.Domain.Dtos.UserDtos;
using electronic.Domain.Models;
using electronic.Domain.Requests;

namespace electronic.Application.Abstracts
{
    public interface IAuthService
    {
        Task<ResponseModel<UserDTO>> Register(UserDTO dto);
        Task<ResponseModel<UserDTO>> Login(LoginDTO dto);
    }
}
