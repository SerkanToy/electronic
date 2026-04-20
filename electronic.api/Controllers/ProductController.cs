using electronic.Application.Interface;
using electronic.Application.Services;
using electronic.Domain.Entities.Employees;
using electronic.Infrastructure.Services;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
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
