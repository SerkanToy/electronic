using electronic.api.Areas.Api.Controllers;
using electronic.Domain.DTOs.Products;
using electronic.Domain.DTOs.SubCategoryDtos;
using electronic.Domain.Entities.Employees.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace electronic.api.Areas.Client.Controllers
{
    [Area("Client")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class SubCategoriesController : ApiBaseController
    {
        [HttpGet]
        [ActionName("subcategory-list")]
        public async Task<ActionResult<SubCategoryDTOs>> Gets()
        {
            var productsDto = await unitOfWork.GetRepository<SubCategories>().GetAllAsyncWithInclude(asNoTracking: false,includeExpression:x => x.Include(f => f.Categories).Include(c => c.Products));
            var productsDtos = productsDto.Select(v => new SubCategoryDTOs
            {
                Id = v.Id.ToString(),
                Name = v.Name,
                categoryName = v.Categories.Name,
                productsDtos = v.Products.Select(x => new ProductsDto { Id = x.Id.ToString(), Name =x.Name })
            });
            return Ok(productsDtos);
        }
    }
}
