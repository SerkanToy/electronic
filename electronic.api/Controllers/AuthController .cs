using electronic.Domain.DTOs.AuthDTOs;
using electronic.Infrastructure.Models;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Controllers
{
    [Route("auth/[action]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        [HttpGet]
        [ActionName("users")]
        public async Task<ActionResult<ResponseModel<IEnumerable<UserApp>>>> Get([FromServices] ResponseModel<IEnumerable<UserApp>> responseModel)
        {
            responseModel.Data = userManager.Users.ToList();
            responseModel.IsSuccess = true;
            responseModel.Message = new List<string> { "Users retrieved successfully." };
            return await Task.FromResult(responseModel);
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<ActionResult<ResponseModel>> Login([FromServices] ResponseModel responseModel, [FromBody] LoginDto dto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    responseModel.Data = dto;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Ok(responseModel);
                }

                return Ok(await authService.Login(dto));
            }
            catch (Exception ex) 
            {
                responseModel.Data = dto;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                return BadRequest(responseModel);
            }
        }

        [HttpPost]
        [ActionName("register-user")]
        public async Task<ActionResult<ResponseModel>> CreateUser([FromBody] RegisterDTO newUser, [FromServices] ResponseModel responseModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    responseModel.Data = newUser;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Ok(responseModel);
                }

                var userApp = new UserApp
                {
                    Email = newUser.Email,
                    Name = newUser.FirstName,
                    SurName = newUser.LastName,
                    PhoneNumber = newUser.PhoneNumber,
                    UserName = newUser.Email,
                    Salt = Guid.NewGuid().ToString(), // Example salt generation, should be handled securely
                };
                userApp.CreateUserId = userApp.Id;


                return Ok(await authService.Register(user: userApp, passwordHash: newUser.Password));
            }
            catch (Exception ex) 
            {
                responseModel.Data = newUser;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                return BadRequest(responseModel);
            }
            
        }
    }
}
