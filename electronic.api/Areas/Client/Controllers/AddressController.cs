using electronic.api.Areas.Api.Controllers;
using electronic.api.Models;
using electronic.Domain.DTOs.AddressesDTOs;
using electronic.Domain.Entities.Employees.Addresses;
using electronic.Infrastructure.Models;
using electronic.Infrastructure.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace electronic.api.Areas.Client.Controllers
{
    [Authorize]
    [Area("Client")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class AddressController : ApiBaseController
    {
        [HttpGet]
        [ActionName("addresses")]
        public async Task<ActionResult<ResponseModel<ClientAddressAllDto>>> All([FromServices] ResponseModel<IEnumerable<ClientAddressAllDto>> responseModel)
        {
            try
            {
                var addresses = await unitOfWork.GetRepository<Addresses>()
                        .GetAllAsyncWithInclude(asNoTracking: false, predicate: c => c.UserAppId == GetUserId(), includeExpression:i => i.Include(C => C.Country).Include(d => d.City).Include(t => t.Town));
                responseModel.Data = addresses.Select(c => new ClientAddressAllDto
                    {
                        Title = c.Title,
                        FullAddress = c.FullAddress,
                        CountryName = c.Country.Name,
                        CityName = c.City.Name.CustomToTitleCase(),
                        MailCode = c.MailCode,
                        TownName = c.Town.Name
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


        [HttpPost]
        [ActionName("addresses-add")]
        public async Task<ActionResult<ResponseModel<ClientAddressAddDto>>> Add([FromServices] ResponseModel<ClientAddressAddDto> responseModel, [FromBody] ClientAddressAddDto newAddress)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.Data = newAddress;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }


                await unitOfWork.GetRepository<Addresses>().AddAsync(new Addresses
                {
                    Title = newAddress.Title,
                    FullAddress = newAddress.FullAddress,
                    MailCode = newAddress.MailCode,
                    UserAppId = GetUserId(),
                    CountryId = Guid.Parse(newAddress.CountryId),
                    TownId = Guid.Parse(newAddress.TownId),
                    CityId = Guid.Parse(newAddress.CityId),
                    CreateUserId = GetUserId()
                });

                var isSuc = await unitOfWork.SaveChangesAsync();

                if(isSuc > 0)
                {
                    responseModel.Data = newAddress;
                    responseModel.IsSuccess = true;
                    responseModel.Message = new List<string> { "Kayıt İşlemi Başarılı." };
                    return Ok(responseModel);
                }
                responseModel.Data = newAddress;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Kayıt İşlemi Başarısız." };


                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = newAddress;
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
