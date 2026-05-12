using electronic.Application.Abstracts;
using electronic.Domain.Dtos.Login;
using electronic.Domain.Dtos.UserDtos;
using electronic.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Controllers
{
    [Route("auth/[action]")]
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
        public async Task<ActionResult<ResponseModel>> Login([FromServices] ResponseModel responseModel,[FromBody] LoginDTO loginDTO)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    responseModel.status = false;
                    responseModel.data = loginDTO;
                    responseModel.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }

                var result = await authService.Login(loginDTO);
                if (result.status)
                {
                    responseModel.status = true;
                    responseModel.data = loginDTO;
                    return Ok(responseModel);
                }

                responseModel.status = false;
                responseModel.errors = new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(responseModel);
            }
            catch (Exception ex) 
            {
                responseModel.status = false;
                responseModel.errors = ex.Message != null ? new List<string> { ex.Message } : new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(responseModel);
            }
        }

        [HttpPost]
        [ActionName("kayit-yap")]
        public async Task<IActionResult> Register([FromServices] ResponseModel responseModel, [FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var result = await authService.Register(registerDTO);
                if (result.status)
                {
                    responseModel.status = true;
                    responseModel.data = registerDTO;
                    return Ok(responseModel);
                }
                responseModel.status = false;
                responseModel.data = registerDTO;
                responseModel.errors = new List<string> { "Kayıt işlemi başarısız oldu." };
                return BadRequest(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.status = false;
                responseModel.errors = ex.Message != null ? new List<string> { ex.Message } : new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(responseModel); 
            }
        }
    }
}
