using electronic.Application.Interface;
using electronic.Domain.Dtos.AddressDto;
using electronic.Domain.Entities.Employees.Address;
using electronic.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace electronic.api.Controllers
{
    [Route("adres/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IGenericRepository<Address> addressGenericRepository;
        private readonly ResponseModel model;
        public AddressController(IGenericRepository<Address> addressGenericRepository = null, ResponseModel model = null)
        {
            this.addressGenericRepository = addressGenericRepository;
            this.model = model;
        }

        [HttpGet]
        [ActionName("adres-listesi")]
        public async Task<ActionResult<ResponseModel>> GetAll([FromServices] ResponseModel responseModel)
        {
            try
            {
                var address = await addressGenericRepository.GetAllAsync();
                model.status = true;
                model.data = address.Select(p => new AddressListDTO { Title = p.Title, FullAddress = p.FullAddress, Id = p.Id }).ToList();
                return Ok(model);
            }
            catch (Exception ex)
            {
                model.status = false;
                model.message = "İstenmeyen Bir Hata Oluştu.";
                model.errors = ex.Message != null ? new List<string> { ex.Message } : new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(model);
            }
        }

        [HttpPost]
        [ActionName("adres-ekle")]
        public async Task<ActionResult<ResponseModel>> Add([FromServices] ResponseModel responseModel, [FromBody] AddressAddDto addressAddDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.status = false;
                    model.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(model);
                }

                Guid CreateUserId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                await addressGenericRepository.CreateAsync(
                    new Address { Title = $"{addressAddDto.Title}", FullAddress = $"{addressAddDto.FullAddress}", MailCode = $"{addressAddDto.MailCode}", CreateUserId = CreateUserId, UserAppId = CreateUserId }
                );
                var result = await addressGenericRepository.SaveChangesAsync();

                if (result != 1)
                {
                    responseModel.status = true;
                    responseModel.data = new AddressAddDto { Title = $"{addressAddDto.Title}", FullAddress = $"{addressAddDto.FullAddress}", MailCode = $"{addressAddDto.MailCode}" };
                    responseModel.errors = new List<string> { "Adress eklenirken bir hata oluştu." };

                    return responseModel;
                }

                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.status = false;
                responseModel.errors = ex.Message != null ? new List<string> { ex.Message } : new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(responseModel);
            }
        }

        [HttpGet("{id?}")]
        [ActionName("adres-detay")]
        public async Task<ActionResult<ResponseModel>> GetById([FromServices] ResponseModel responseModel, [Required(ErrorMessage = "Geçersiz işlem.")] string id = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.status = false;
                    model.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(model);
                }
                /*if (string.IsNullOrEmpty(id))
                {
                    //context.Response.WriteAsJsonAsync(new { message = "Geçersiz işlem." });
                    responseModel.status = false;
                    responseModel.message = "Geçersiz işlem.";
                    return responseModel;
                }*/
                var address = await addressGenericRepository.GetByIdAsync(Guid.Parse(id));

                if (address == null)
                {
                    responseModel.status = false;
                    responseModel.message = "Geçerli bir kategori bulunamadı.";
                    return responseModel;
                }


                responseModel.status = false;
                responseModel.data = new AddressByIdDto
                {
                    Id = address.Id,
                    Title = address.Title,
                    FullAddress = address.FullAddress
                };
                return responseModel;
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
