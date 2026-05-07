using electronic.Application.Interface;
using electronic.Domain.Dtos.CategoriDto;
using electronic.Domain.Entities.Employees;
using electronic.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Controllers
{
    [Route("kategori/[action]")]
    [ApiController]
    public class CategoriController : ControllerBase
    {
        private readonly IGenericRepository<Categori> categoriGenericRepository;
        private HttpContext context;
        public CategoriController(IGenericRepository<Categori> categoriGenericRepository = null, HttpContext context = null) { 
            this.categoriGenericRepository = categoriGenericRepository;
            this.context = context;
        }

        [HttpGet]
        [ActionName("kategori-listesi")]
        public async Task<ActionResult<ResponseModel<CategoriListDTO>>> GetAll([FromServices] ResponseModel<CategoriListDTO> responseModel)
        {
            var categori = await categoriGenericRepository.GetAllAsync();            
            responseModel.IsSuccess = true;
            responseModel.DataList = categori.Select(p => new CategoriListDTO { Name = p.Name, Description = p.Description, icon = p.icon, CreateBy = p.CreateUserName }).ToList();
            return Ok(responseModel);
        }

        [HttpPost]
        [ActionName("kategori-ekle")]
        public async Task<ResponseModel<CategoriDTO>> Add([FromServices] ResponseModel<CategoriDTO> responseModel, [FromBody] AddCategoriDTO addCategoriDTO)
        {
            await categoriGenericRepository.CreateAsync(
                new Categori { Name = $"{addCategoriDTO.Name}", Description = $"{addCategoriDTO.Description}", icon = $"{addCategoriDTO.icon}" } 
            );
            var result = await categoriGenericRepository.SaveChangesAsync();

            if(result != 1)
            {
                responseModel.IsSuccess = false;
                responseModel.Data = new CategoriDTO { Name = $"{addCategoriDTO.Name}", Description = $"{addCategoriDTO.Description}", icon = $"{addCategoriDTO.icon}" };
                responseModel.Message = new List<string> { "Kategori eklenirken bir hata oluştu." };

                return responseModel;
            }

            return responseModel;
        }

        [HttpGet("{id?}")]
        [ActionName("kategori-detay")]
        public async Task<ResponseModel<CategoriByIdDto>> GetById([FromServices] ResponseModel<CategoriByIdDto> responseModel, string id = null)
        {
            //context.Request.Headers.
            if (string.IsNullOrEmpty(id))
            {
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Geçersiz işlem." };
                return responseModel;
            }
            var categori = await categoriGenericRepository.GetByIdAsync(Guid.Parse(id));

            if(categori == null)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Geçerli bir kategori bulunamadı." };
                return responseModel;
            }


            responseModel.IsSuccess = true;
            responseModel.Data = new CategoriByIdDto
            {
                Name = categori.Name,
                Description = categori.Description,
                icon = categori.icon,
                CreateBy = categori.CreateUserName
            };
            return responseModel;
        }

    }
}
