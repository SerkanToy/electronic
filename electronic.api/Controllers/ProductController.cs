using electronic.Application.Interface;
using electronic.Domain.Entities.Employees;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Controllers
{
    [Route("urun/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> productGenericRepository;
        public ProductController(IGenericRepository<Product> productGenericRepository = null) { 
            this.productGenericRepository = productGenericRepository;
        }

        [HttpGet]
        [ActionName("urunler")]
        public List<Product> GetAll()
        {
            return productGenericRepository.GetAll().ToList();
        }
    }
}
