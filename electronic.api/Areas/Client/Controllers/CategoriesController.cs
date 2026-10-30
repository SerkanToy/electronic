using electronic.api.Areas.Api.Controllers;
using electronic.Domain.DTOs.CategoryDTOs;
using electronic.Domain.DTOs.SubCategoryDtos;
using electronic.Domain.Entities.Employees.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace electronic.api.Areas.Client.Controllers
{
    [Area("Client")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ApiBaseController
    {
        [HttpGet]
        [ActionName("category-list")]
        public async Task<ActionResult<CategoryDto>> Gets()
        {
            var categoryDto = await unitOfWork.GetRepository<Categories>().GetAllAsyncWithInclude(includeExpression:f => f.Include(v => v.SubCategories),asNoTracking: false);
            var categoryDtos = categoryDto.Select(v => new CategoryDto
            {
                Id = v.Id.ToString(),
                SubCategoryDTOs = v.SubCategories.Select(b => new SubCategoryDTOs { Name = b.Name, Id = b.Id.ToString()}),
                Name = v.Name,
            });
            return Ok(categoryDtos);
        }
    }
}
