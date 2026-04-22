using electronic.Domain.Requests;

namespace electronic.Application.Abstracts
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterRequest registerRequest);
        Task LoginAsync(LoginRequest loginRequest);
        Task RefreshTokenAsync(string? refreshToken);
    }
}
