using electronic.api.Areas.Api.Controllers;
using electronic.Domain.DTOs.PhotographDTOs;
using electronic.Domain.Entities.Employees;
using electronic.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Areas.Client.Controllers
{
    [Area("Client")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class PhotographController : ApiBaseController
    {
        [HttpGet]
        [ActionName("photograph-list")]
        public async Task<ActionResult<ResponseModel<ClientPhotographAllDto>>> List([FromServices] ResponseModel responseModel)
        {
            try
            {
                var photograph = await unitOfWork.GetRepository<ProductImages>().GetAllAsync(asNoTracking: false);

                var photographDto = photograph.Select(x => new ClientPhotographAllDto
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    ProductId = x.ProductId.ToString(),
                }).ToList();

                responseModel.Data = photographDto;
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { "İşlemi Başarılı" };
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
