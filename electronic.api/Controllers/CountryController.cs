using electronic.Application.Interface;
using electronic.Domain.Dtos.CountryDto;
using electronic.Domain.Entities.Employees.Address;
using electronic.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace electronic.api.Controllers
{
    [Route("ulke/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IGenericRepository<Country> countryGenericRepository;
        public CountryController(IGenericRepository<Country> countryGenericRepository = null) { 
            this.countryGenericRepository = countryGenericRepository;
        }

        [HttpGet]
        [ActionName("ulke-listesi")]
        public async Task<ActionResult<ResponseModel>> GetAll([FromServices] ResponseModel responseModel)
        {
            try
            {
                var categori = await countryGenericRepository.GetAllAsync();
                responseModel.status = true;
                responseModel.data = categori.Select(p => new CountryListDTO { Id = p.Id, Name = p.Name }).ToList();
                return Ok(responseModel);
            }
            catch (Exception ex) 
            {
                responseModel.status = false;
                responseModel.message = "İstenmeyen Bir Hata Oluştu.";
                responseModel.errors = ex.Message != null ? new List<string> { ex.Message } : new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(responseModel);
            }
        }

        [HttpPost]
        [ActionName("ulke-ekle")]
        public async Task<ActionResult<ResponseModel>> Add([FromServices] ResponseModel model, [FromBody] CountryAddDto countryAddDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    model.status = false;
                    model.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(model);
                }

                await countryGenericRepository.CreateAsync(
                    new Country { Name = $"{countryAddDto.Name}" }
                );
                var result = await countryGenericRepository.SaveChangesAsync();

                if (result == 1)
                {
                    model.status = true;
                    model.data = new CountryAddDto { Name = $"{countryAddDto.Name}"  };
                    model.errors = new List<string> { "İşlem Başarılı" };

                    return Ok(model);
                }

                model.status = false;
                model.data = new CountryAddDto { Name = $"{countryAddDto.Name}" };
                model.errors = new List<string> { "Kategori eklenirken bir hata oluştu." };

                return BadRequest(model);
            }
            catch (Exception ex) 
            {
                model.status = false;
                model.errors = ex.Message != null ? new List<string> { ex.Message } : new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(model);
            }
            
        }

        [HttpGet("{id?}")]
        [ActionName("ulke-detay")]
        public async Task<ActionResult<ResponseModel>> GetById([FromServices] ResponseModel responseModel,[Required(ErrorMessage = "Geçersiz işlem.")] string id = null)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    responseModel.status = false;
                    responseModel.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }
                /*if (string.IsNullOrEmpty(id))
                {
                    //context.Response.WriteAsJsonAsync(new { message = "Geçersiz işlem." });
                    responseModel.status = false;
                    responseModel.message = "Geçersiz işlem.";
                    return responseModel;
                }*/
                var categori = await countryGenericRepository.GetByIdAsync(Guid.Parse(id));

                if (categori == null)
                {
                    responseModel.status = false;
                    responseModel.message = "Geçerli bir Ülke bulunamadı.";
                    return Ok(responseModel);
                }


                responseModel.status = true;
                responseModel.data = new CountryByIdDto
                {
                    Name = categori.Name
                };
                return Ok(responseModel);
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
