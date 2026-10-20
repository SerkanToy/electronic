using electronic.api.Areas.Api.Controllers;
using electronic.Domain.DTOs.Products;
using electronic.Domain.Entities.Employees.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace electronic.api.Areas.Client.Controllers
{
    [Area("Client")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        [HttpGet]
        [ActionName("product-list")]
        public async Task<ActionResult<ProductsDto>> Gets()
        {
            var productsDto = await unitOfWork.GetRepository<Products>().GetAllAsyncWithInclude(asNoTracking: false,includeExpression:x => x.Include(f => f.Categories).ThenInclude(f => f.SubCategories));
            var productsDtos = productsDto.Select(v => new ProductsDto
            {
                Id = v.Id.ToString(),
                Name = v.Name,
                categoryName = v.Categories.Name,
                subCategoryName = v.SubCategories.Name

            });
            return Ok(productsDtos);
        }
    }
}
