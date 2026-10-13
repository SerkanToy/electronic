using electronic.api.Areas.Api.Controllers;
using electronic.Domain.DTOs.CategoryDTOs;
using electronic.Domain.Entities.Employees.Category;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Areas.Client.Controllers
{
    [Area("Client")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<CategoryDto>> SectionGet()
        {
            var categoryDto =  await unitOfWork.GetRepository<Categories>().GetAllAsync(asNoTracking:false);
            var categoryDtos = categoryDto.Select(v => new CategoryDto
            {
                Id = v.Id.ToString(),
                Name = v.Name,
            });
            return Ok(categoryDtos);
        }
    }
}
