using electronic.api.Areas.Api.Controllers;
using electronic.api.Models;
using electronic.Domain.DTOs.AuthDTOs;
using electronic.Domain.DTOs.CategoryDTOs;
using electronic.Domain.Entities.Employees.Category;
using electronic.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace electronic.api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ApiBaseController
    {
        [HttpGet]
        [ActionName("category-section")]
        public async Task<ActionResult<ResponseModel<AdminCategoryDto>>> SectionGet([FromServices] ResponseModel responseModel)
        {
            try
            {
                var categoryDto = await unitOfWork.GetRepository<Categories>().GetAllAsync(asNoTracking: false);
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
            catch (Exception ex)
            {
                responseModel.Data = null;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                return BadRequest(responseModel);
            }

        }

        [HttpGet]
        [ActionName("category-list")]
        public async Task<ActionResult<ResponseModel<AdminCategoryAllDto>>> CategoryList([FromServices] ResponseModel responseModel)
        {
            try
            {
                var category = await unitOfWork.GetRepository<Categories>().GetAllAsync(asNoTracking:false);

                var categoryDto = category.Select(x => new AdminCategoryAllDto
                {
                    Id = x.Id.ToString(),
                    Name = x.Name.CustomToTitleCase(),
                    icon = x.icon,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CreateUserName = x.CreateUserName.CustomToTitleCase(),
                    DeleteUser = x.DeleteUserName?.CustomToTitleCase(),
                    UpdateUser = x.UpdateUserName?.CustomToTitleCase(),
                    CreateAt = x.CreateAt,
                    DeleteAt = x.DeleteAt,
                    UpdateAt = x.UpdateAt
                }).ToList();

                responseModel.Data = categoryDto;
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { "İşlemi Başarılı" };
                return Ok(responseModel);

            }
            catch (Exception ex)
            {
                responseModel.Data = null;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                foreach (var item in ex.Data)
                {
                    responseModel.Message.Add(item.ToString());
                }
                return BadRequest(responseModel);
            }
        }

        [HttpPost]
        [ActionName("category-add")]
        public async Task<ActionResult<ResponseModel<AdminCategoryAddDto>>> CategoryAdd([FromServices] ResponseModel responseModel, [FromBody] AdminCategoryAddDto newCategory)
        {
            try
            {
                var isCategory = await unitOfWork.GetRepository<Categories>().FindAsync(x => x.Name.ToLower() == newCategory.Name.ToLower(), asNoTracking:false);
                if (isCategory.Count() == 0)
                {
                    await unitOfWork.GetRepository<Categories>().AddAsync(new Categories
                    {
                        Name = newCategory.Name.CustomToTitleCase(),
                        Description = newCategory.Description,
                        icon = newCategory.icon,
                        CreateUserId = Guid.Parse("ff4dbf3c-ce20-4e35-befd-1f1d89bd56d5")
                    });

                    var result = await unitOfWork.SaveChangesAsync();

                    if (result > 0)
                    {
                        responseModel.Data = newCategory;
                        responseModel.IsSuccess = true;
                        responseModel.Message = new List<string> { "Kayıt İşlemi Başarılı" };

                        return Ok(responseModel);
                    }
                }

                responseModel.Data = newCategory;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Kayıt İşlemi Başarısız", "Kategori Adının Olmadığından Emin Olun", "Sistem Yöneticisi İle İletişime Geçin" };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = newCategory;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                foreach (var item in ex.Data)
                {
                    responseModel.Message.Add(item.ToString());
                }
                return BadRequest(responseModel);
            }
        }


        [HttpGet("{id}")]
        [ActionName("category-detail")]
        public async Task<ActionResult<ResponseModel<AdminCategoryDetailDto>>> CategoryDetail([FromServices] ResponseModel responseModel, [Required(ErrorMessage = "Parametre Boş Olamaz")] string id)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    responseModel.Data = null;
                    responseModel.IsSuccess = false;
                    responseModel.Message = new List<string> { "İşlemi Başarısız" };
                    return NotFound(responseModel);
                }

                var category = await unitOfWork.GetRepository<Categories>().GetByIdAsync(x => x.Id == Guid.Parse(id), asNoTracking: false);
                if (category is null)
                {
                    responseModel.Data = null;
                    responseModel.IsSuccess = false;
                    responseModel.Message = new List<string> { "İşlemi Başarısız", "Kategori Bulunamadı." };
                    return Ok(responseModel);                    
                }


                responseModel.Data = new AdminCategoryDetailDto
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    Description = category.Description,
                    IsActive = category.IsActive,
                    IsDeleted = category.IsDeleted,
                    CreateUserName = category.CreateUserName,
                    DeleteUser = category.DeleteUserName,
                    UpdateUser = category.UpdateUserName,
                    CreateAt = category.CreateAt,
                    DeleteAt = category.DeleteAt,
                    UpdateAt = category.UpdateAt
                };
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { "İşlemi Başarılı" };
                return Ok(responseModel);

            }
            catch (Exception ex)
            {
                responseModel.Data = null;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                foreach (var item in ex.Data)
                {
                    responseModel.Message.Add(item.ToString());
                }
                return BadRequest(responseModel);
            }
        }

    }
}
