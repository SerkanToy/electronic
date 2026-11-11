using electronic.api.Areas.Api.Controllers;
using electronic.api.Models;
using electronic.Domain.DTOs.AddressesDTOs;
using electronic.Domain.Entities.Employees.Addresses;
using electronic.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace electronic.api.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class AddressController : ApiBaseController
    {
        [HttpGet("{userId?}")]
        [ActionName("addresses")]
        public async Task<ActionResult<ResponseModel<AdminAddressAllDto>>> All([FromServices] ResponseModel<IEnumerable<AdminAddressAllDto>> responseModel,[Required(ErrorMessage = "*")] string userId)
        {
            try
            {
                var addresses = await unitOfWork.GetRepository<Addresses>()
                        .GetAllAsyncWithInclude(asNoTracking: false, predicate: c => c.UserAppId == Guid.Parse(userId), includeExpression: i => i.Include(C => C.Country).Include(d => d.City).Include(t => t.Town));
                responseModel.Data = addresses.Select(c => new AdminAddressAllDto
                {
                    Title = c.Title,
                    FullAddress = c.FullAddress,
                    CountryName = c.Country.Name,
                    CityName = c.City.Name.CustomToTitleCase(),
                    MailCode = c.MailCode,
                    TownName = c.Town.Name,
                    CreateUserName = c.CreateUserName
                });
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { "Addresses retrieved successfully." };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = null;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                foreach (var item in ex.Data)
                {
                    responseModel.Message.Add(item.ToString());
                }
                return BadRequest(responseModel);
            }
        }
    }
}
