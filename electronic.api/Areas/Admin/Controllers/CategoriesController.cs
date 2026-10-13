using electronic.api.Areas.Api.Controllers;
using electronic.Domain.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<AdminCategoryDto>> SectionGet()
        {
            var categoryDto =  await unitOfWork.GetRepository<AdminCategoryDto>().GetAllAsync(asNoTracking:false);
            var categoryDtos = categoryDto.Select(v => new AdminCategoryDto
            {
                Id = v.Id.ToString(),
                Name = v.Name,
                Description = v.Description,   
                icon = v.icon,
                Products = v.Products,
                SubCategories = v.SubCategories,
            });
            return Ok(categoryDto);
        }
    }
}
