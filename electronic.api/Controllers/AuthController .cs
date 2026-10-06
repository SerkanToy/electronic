using electronic.Domain.DTOs.AuthDTOs;
using electronic.Infrastructure.Models;
using electronik.Domain.Entities.Users;
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
        [ActionName("register-user")]
        public async Task<ActionResult<ResponseModel<RegisterDTO>>> CreateUser([FromBody] RegisterDTO newUser, [FromServices] ResponseModel<RegisterDTO> responseModel)
        {
            var user = new UserApp
            {
                Email = newUser.Email,
                Name = newUser.FirstName,
                SurName = newUser.LastName,
                PhoneNumber = newUser.PhoneNumber,
                UserName = newUser.Email,
                Salt = Guid.NewGuid().ToString(), // Example salt generation, should be handled securely
            };
            user.CreateUserId = user.Id;
            user.Id = user.Id;
            var result = await userManager.CreateAsync(user, newUser.Password); // Example password, should be handled securely
            if (result.Succeeded)
            {
                responseModel.Data = newUser;
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { "User created successfully." };
                return Ok(responseModel);
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(responseModel);
            }
        }
    }
}
