using electronic.Application.Abstracts;
using electronic.Domain.Dtos.Login;
using electronic.Domain.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Controllers
{
    [Route("auth/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [ActionName("giris-yap")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await authService.Login(loginDTO);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [ActionName("kayit-yap")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await authService.Register(registerDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
