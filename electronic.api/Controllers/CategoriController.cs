using electronic.Application.Interface;
using electronic.Domain.Dtos.Categoris;
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
        private readonly ResponseModel<CategoriDTO> responseModel;
        public CategoriController(IGenericRepository<Categori> categoriGenericRepository = null, ResponseModel<CategoriDTO> responseModel = null) { 
            this.categoriGenericRepository = categoriGenericRepository;
            this.responseModel = responseModel;
        }

        [HttpGet]
        [ActionName("kategoriler")]
        public async Task<List<CategoriDTO>> GetAll()
        {
            var products = await categoriGenericRepository.GetAllAsync();
            return products.Select(p => new CategoriDTO { Name = p.Name, Description = p.Description, icon = p.icon, CreateBy = p.CreateUserName }).ToList();
        }

        [HttpGet]
        [ActionName("kategori-ekle")]
        public async Task<ResponseModel<CategoriDTO>> Add([FromBody] AddCategoriDTO addCategoriDTO)
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
    }
}
