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
        //private ResponseModel _responseModel;
        //private ResponseModel<object> _responseModelObject;
        private IUnitOfWork _unitOfWork;
        private IConfiguration _config;

        protected IConfiguration configuration => _config ?? HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
        protected HttpContext httpContext => _httpContext ??= HttpContext;
        protected UserManager<UserApp> userManager => _userManager ??= httpContext.RequestServices.GetService<UserManager<UserApp>>() as UserManager<UserApp>;

        //protected ResponseModel responseModel => _responseModel ??= httpContext.RequestServices.GetService<ResponseModel>() as ResponseModel;
        //protected ResponseModel<object> responseModelObject => _responseModelObject ??= httpContext.RequestServices.GetService(typeof(ResponseModel<object>)) as ResponseModel<object>;
        protected IUnitOfWork unitOfWork  => _unitOfWork ??= httpContext.RequestServices.GetService<IUnitOfWork>();
    }
}
