using electronic.Application.Interface;
using electronic.Domain.Dtos.ProductDto;
using electronic.Domain.Entities.Employees;
using electronic.Domain.Exceptions;
using electronic.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ResponseModel<ProductListDto>> GetAll([FromServices] ResponseModel<ProductListDto> responseModel)
        {
            var products = await productGenericRepository.GetAllAsync();
            responseModel.IsSuccess = true;
            responseModel.DataList = products.Select(v => new ProductListDto
                                        {
                                            Id = v.Id,
                                            Name = v.Name,
                                            Description = v.Description,
                                            RegulerPrice = v.RegulerPrice,
                                            DiscountPrice = v.DiscountPrice,
                                            Note = v.Note,
                                            Icon = v.icon
                                        }).ToList();
            return responseModel;
        }


        [HttpGet("{id}")]
        [ActionName("urun-detay")]
        public async Task<ResponseModel<ProductByIdDto>> GetById([FromServices] ResponseModel<ProductByIdDto> responseModel, string id)
        {
            var products = await productGenericRepository.GetByIdAsync(Guid.Parse(id));
            responseModel.IsSuccess = true;
            responseModel.Data = new ProductByIdDto
                                {
                                    Id = products.Id,
                                    Name = products.Name,
                                    Description = products.Description,
                                    RegulerPrice = products.RegulerPrice,
                                    DiscountPrice = products.DiscountPrice,
                                    Note = products.Note,
                                    Icon = products.icon
                                };
            return responseModel;
        }

        [HttpPost]
        [ActionName("urun-ekle")]
        public async Task<ActionResult<ResponseModel<ProductCreateDto>>> Create([FromServices] ResponseModel<ProductCreateDto> responseModel, ProductCreateDto productCreateDto)
        {
            if(!ModelState.IsValid)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                responseModel.Data = productCreateDto;
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
                CreateUserId = Guid.Parse("019df808-de80-7352-8987-fc6f0c56e282")

            };
            await productGenericRepository.CreateAsync(product);

            await productJoinCategori.CreateAsync(new ProductJoinCategori
            {
                ProductId = product.Id,
                //CategoriId = productCreateDto.CategoryId
            });

            var save = await productGenericRepository.SaveChangesAsync();

            if(save != 0)
            {
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { ErrorsText.Success };
                responseModel.Data = productCreateDto;
                return Ok(responseModel);
            }

            responseModel.IsSuccess = false;
            responseModel.Message = new List<string> { ErrorsText.SaveError }; //ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Ok(responseModel);
        }

        [HttpPut("{id}")]
        [ActionName("urun-guncelle")]
        public async Task<ResponseModel<ProductUpdateDto>> UpdateAsync([FromServices] ResponseModel<ProductUpdateDto> responseModel, 
                                                            ProductUpdateDto productUpdateDto, 
                                                            string id)
        {
            var products = await productGenericRepository.GetByIdAsync(Guid.Parse(id));
            return responseModel;
        }


        [HttpDelete("{id}")]
        [ActionName("urun-sil")]
        public async Task<ResponseModel<ProductDeleteDto>> DeleteAsync([FromServices] ResponseModel<ProductDeleteDto> responseModel, string id)
        {
            var products = await productGenericRepository.GetByIdAsync(Guid.Parse(id));
            products.IsDeleted = true;
            productGenericRepository.Update(products);
            var save = await productGenericRepository.SaveChangesAsync();

            if(save == 0)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ErrorsText.SaveError };
                responseModel.Data = new ProductDeleteDto
                {

                };
                return responseModel;
            }

            responseModel.IsSuccess = true;
            responseModel.Message = new List<string> { ErrorsText.Success };
            responseModel.Data = new ProductDeleteDto
            {

            };

            return responseModel;
        }
    }
}
