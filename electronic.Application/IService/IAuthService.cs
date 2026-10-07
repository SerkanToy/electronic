using electronic.Domain.DTOs.AuthDTOs;
using electronic.Infrastructure.Models;
using electronik.Domain.Entities.Users;

namespace electronic.Application.IService
{
    public interface IAuthService
    {
        Task<ResponseModel> Login(LoginDto dto);
        Task<ResponseModel> Register(UserApp user, string passwordHash);
    }
}
