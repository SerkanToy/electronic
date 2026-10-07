using electronic.Application.IService;
using electronic.Application.UoW;
using electronic.Infrastructure.Models;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        private HttpContext _httpContext;
        private UserManager<UserApp> _userManager;
        private IAuthService _authService;
        private ITokenService _tokenService;
        private IUnitOfWork _unitOfWork;
        private IConfiguration _config;

        protected IConfiguration configuration => _config ?? HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
        protected HttpContext httpContext => _httpContext ??= HttpContext;
        protected UserManager<UserApp> userManager => _userManager ??= httpContext.RequestServices.GetService<UserManager<UserApp>>() as UserManager<UserApp>;

        protected IUnitOfWork unitOfWork  => _unitOfWork ??= httpContext.RequestServices.GetService<IUnitOfWork>();
        protected IAuthService authService => _authService ??= httpContext.RequestServices.GetService<IAuthService>();
        protected ITokenService tokenService => _tokenService ??= httpContext.RequestServices.GetService<ITokenService>();
    }
}
