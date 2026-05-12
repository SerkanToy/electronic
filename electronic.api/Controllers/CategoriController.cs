using electronic.Application.Interface;
using electronic.Domain.Dtos.CategoriDto;
using electronic.Domain.Entities.Employees;
using electronic.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;

namespace electronic.api.Controllers
{
    [Route("kategori/[action]")]
    [ApiController]
    public class CategoriController : ControllerBase
    {
        private readonly IGenericRepository<Categori> categoriGenericRepository;
        private readonly ResponseModel model;
        public CategoriController(IGenericRepository<Categori> categoriGenericRepository = null, ResponseModel model = null) { 
            this.categoriGenericRepository = categoriGenericRepository;
            this.model = model;
        }

        [HttpGet]
        [ActionName("kategori-listesi")]
        public async Task<ActionResult<ResponseModel>> GetAll([FromServices] ResponseModel responseModel)
        {
            try
            {
                var categori = await categoriGenericRepository.GetAllAsync();
                model.status = true;
                model.data = categori.Select(p => new CategoriListDTO { Name = p.Name, Description = p.Description, icon = p.icon, CreateBy = p.CreateUserName }).ToList();
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
        [ActionName("kategori-ekle")]
        public async Task<ActionResult<ResponseModel>> Add([FromServices] ResponseModel responseModel, [FromBody] AddCategoriDTO addCategoriDTO)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    model.status = false;
                    model.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(model);
                }

                await categoriGenericRepository.CreateAsync(
                    new Categori { Name = $"{addCategoriDTO.Name}", Description = $"{addCategoriDTO.Description}", icon = $"{addCategoriDTO.icon}" }
                );
                var result = await categoriGenericRepository.SaveChangesAsync();

                if (result != 1)
                {
                    responseModel.status = true;
                    responseModel.data = new CategoriDTO { Name = $"{addCategoriDTO.Name}", Description = $"{addCategoriDTO.Description}", icon = $"{addCategoriDTO.icon}" };
                    responseModel.errors = new List<string> { "Kategori eklenirken bir hata oluştu." };

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
        [ActionName("kategori-detay")]
        public async Task<ActionResult<ResponseModel>> GetById([FromServices] ResponseModel responseModel,[Required(ErrorMessage = "Geçersiz işlem.")] string id = null)
        {
            try
            {
                if(!ModelState.IsValid)
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
                var categori = await categoriGenericRepository.GetByIdAsync(Guid.Parse(id));

                if (categori == null)
                {
                    responseModel.status = false;
                    responseModel.message = "Geçerli bir kategori bulunamadı.";
                    return responseModel;
                }


                responseModel.status = false;
                responseModel.data = new CategoriByIdDto
                {
                    Name = categori.Name,
                    Description = categori.Description,
                    icon = categori.icon,
                    CreateBy = categori.CreateUserName
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
