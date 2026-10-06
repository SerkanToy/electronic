using electronic.Domain.DTOs.AuthDTOs;
using electronic.Infrastructure.Models;

namespace electronic.Application.IService
{
    public interface IAuthService
    {
        Task<ResponseModel> Login(LoginDto dto);
        Task<ResponseModel> Register(RegisterDTO dto);
    }
}
