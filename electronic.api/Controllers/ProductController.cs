using electronic.Application.Interface;
using electronic.Domain.Dtos.ProductDto;
using electronic.Domain.Entities.Employees;
using electronic.Domain.Exceptions;
using electronic.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace electronic.api.Controllers
{
    [Route("urunler/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> productGenericRepository;
        private readonly IGenericRepository<ProductJoinCategori> productJoinCategori;
        public ProductController(IGenericRepository<Product> productGenericRepository = null,
            IGenericRepository<ProductJoinCategori> productJoinCategori = null) { 
            this.productGenericRepository = productGenericRepository;
            this.productJoinCategori = productJoinCategori;
        }

        [HttpGet]
        [ActionName("urun-listesi")]
        public async Task<ActionResult<ResponseModel>> GetAll([FromServices] ResponseModel responseModel)
        {
            try
            {
                var products = await productGenericRepository.GetAllAsync();
                responseModel.status = true;
                responseModel.data = products.Select(v => new ProductListDto
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    RegulerPrice = v.RegulerPrice,
                    DiscountPrice = v.DiscountPrice,
                    Note = v.Note,
                    Icon = v.icon
                }).ToList();
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


        [HttpGet("{id}")]
        [ActionName("urun-detay")]
        public async Task<ActionResult<ResponseModel>> GetById([FromServices] ResponseModel responseModel,[Required(ErrorMessage = "Geçersiz işlem.")] string id)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    responseModel.status = false;
                    responseModel.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }
                var products = await productGenericRepository.GetByIdAsync(Guid.Parse(id));
                responseModel.status = true;
                responseModel.data = new ProductByIdDto
                {
                    Id = products.Id,
                    Name = products.Name,
                    Description = products.Description,
                    RegulerPrice = products.RegulerPrice,
                    DiscountPrice = products.DiscountPrice,
                    Note = products.Note,
                    Icon = products.icon,
                    
                };
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
        [ActionName("urun-ekle")]
        public async Task<ActionResult<ResponseModel>> Create([FromServices] ResponseModel responseModel, ProductCreateDto productCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.status = false;
                    responseModel.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    responseModel.data = productCreateDto;
                    return BadRequest(ModelState);
                }

                Product product = new Product
                {
                    Name = productCreateDto.Name,
                    Description = productCreateDto.Description,
                    RegulerPrice = productCreateDto.RegulerPrice,
                    DiscountPrice = productCreateDto.DiscountPrice,
                    Note = productCreateDto.Note,
                    icon = productCreateDto.icon,
                    CreateUserId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value)

                };
                await productGenericRepository.CreateAsync(product);

                await productJoinCategori.CreateAsync(new ProductJoinCategori
                {
                    ProductId = product.Id,
                    //CategoriId = productCreateDto.CategoryId
                });



                var save = await productGenericRepository.SaveChangesAsync();

                if (save != 0)
                {
                    responseModel.status = false;
                    responseModel.errors = new List<string> { ErrorsText.Success };
                    responseModel.data = productCreateDto;
                    return Ok(responseModel);
                }

                responseModel.status = true;
                responseModel.errors = new List<string> { ErrorsText.SaveError }; //ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Ok(responseModel);

            }
            catch(Exception ex)
            {
                responseModel.status = false;
                responseModel.message = "İstenmeyen Bir Hata Oluştu.";
                responseModel.errors = ex.Message != null ? new List<string> { ex.Message } : new List<string> { "İstenmeyen Bir Hata Oluştu." };
                return BadRequest(responseModel);
            }
        }

        [HttpPut("{id}")]
        [ActionName("urun-guncelle")]
        public async Task<ResponseModel> UpdateAsync([FromServices] ResponseModel responseModel, 
                                                            ProductUpdateDto productUpdateDto, 
                                                            string id)
        {
            var products = await productGenericRepository.GetByIdAsync(Guid.Parse(id));
            return responseModel;
        }


        [HttpDelete("{id}")]
        [ActionName("urun-sil")]
        public async Task<ActionResult<ResponseModel>> DeleteAsync([FromServices] ResponseModel responseModel,[Required(ErrorMessage = "Boş Bırakmayın.")] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.status = false;
                    responseModel.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(ModelState);
                }

                var products = await productGenericRepository.GetByIdAsync(Guid.Parse(id));
                products.IsDeleted = true;
                productGenericRepository.Update(products);
                var save = await productGenericRepository.SaveChangesAsync();

                if (save == 0)
                {
                    responseModel.status = false;
                    responseModel.errors = new List<string> { ErrorsText.SaveError };
                    responseModel.data = new ProductDeleteDto
                    {

                    };
                    return responseModel;
                }

                responseModel.status = true;
                responseModel.data = new ProductDeleteDto
                {

                };

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
    }
}
