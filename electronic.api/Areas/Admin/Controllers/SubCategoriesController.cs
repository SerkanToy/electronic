using electronic.api.Areas.Api.Controllers;
using electronic.api.Models;
using electronic.Domain.DTOs.SubCategoryDtos;
using electronic.Domain.Entities.Employees.Category;
using electronic.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace electronic.api.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class SubCategoriesController : ApiBaseController
    {
        [HttpGet]
        [ActionName("subcategory-list")]
        public async Task<ActionResult<ResponseModel<AdminSubCategoryAllDto>>> List([FromServices] ResponseModel responseModel)
        {
            try
            {
                var products = await unitOfWork.GetRepository<SubCategories>().GetAllAsync(asNoTracking: false);

                var productDto = products.Select(x => new AdminSubCategoryAllDto
                {
                    Id = x.Id.ToString(),
                    Name = x.Name.CustomToTitleCase(),
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CreateUserName = x.CreateUserName.CustomToTitleCase(),
                    DeleteUser = x.DeleteUserName?.CustomToTitleCase(),
                    UpdateUser = x.UpdateUserName?.CustomToTitleCase(),
                    CreateAt = x.CreateAt,
                    DeleteAt = x.DeleteAt,
                    UpdateAt = x.UpdateAt
                }).ToList();

                responseModel.Data = productDto;
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
        [ActionName("subcategory-add")]
        public async Task<ActionResult<ResponseModel<AdminSubCategoriesAddDto>>> Add([FromServices] ResponseModel responseModel, [FromBody] AdminSubCategoriesAddDto newSubCategories)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.Data = newSubCategories;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }

                var isSubCategories = await unitOfWork.GetRepository<SubCategories>().FindAsync(x => x.Name.ToLower() == newSubCategories.Name.ToLower(), asNoTracking: false);
                if (isSubCategories.Count() == 0)
                {
                    await unitOfWork.GetRepository<SubCategories>().AddAsync(new SubCategories
                    {
                        Name = newSubCategories.Name.CustomToTitleCase(),
                        CreateUserId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                    });

                    var result = await unitOfWork.SaveChangesAsync();

                    if (result > 0)
                    {
                        responseModel.Data = newSubCategories;
                        responseModel.IsSuccess = true;
                        responseModel.Message = new List<string> { "Kayıt İşlemi Başarılı" };

                        return Ok(responseModel);
                    }
                }

                responseModel.Data = newSubCategories;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Kayıt İşlemi Başarısız", "Alt Kategori Adının Olmadığından Emin Olun", "Sistem Yöneticisi İle İletişime Geçin" };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = newSubCategories;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                foreach (var item in ex.Data)
                {
                    responseModel.Message.Add(item.ToString());
                }
                return BadRequest(responseModel);
            }
        }

        [HttpGet("{id?}")]
        [ActionName("subcategory-detail")]
        public async Task<ActionResult<ResponseModel<AdminSubCategoriesDetailDto>>> Detail([FromServices] ResponseModel responseModel, [Required(ErrorMessage = "Parametre Boş Olamaz")] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.Data = null;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }

                var subCategories = await unitOfWork.GetRepository<SubCategories>().GetByIdAsync(x => x.Id == Guid.Parse(id), asNoTracking: false);
                if (subCategories is null)
                {
                    responseModel.Data = null;
                    responseModel.IsSuccess = false;
                    responseModel.Message = new List<string> { "İşlemi Başarısız", "Ürün Bulunamadı." };
                    return Ok(responseModel);
                }


                responseModel.Data = new AdminSubCategoriesDetailDto
                {
                    Id = subCategories.Id.ToString(),
                    Name = subCategories.Name,
                    IsActive = subCategories.IsActive,
                    IsDeleted = subCategories.IsDeleted,
                    CreateUserName = subCategories.CreateUserName,
                    DeleteUser = subCategories.DeleteUserName,
                    UpdateUser = subCategories.UpdateUserName,
                    CreateAt = subCategories.CreateAt,
                    DeleteAt = subCategories.DeleteAt,
                    UpdateAt = subCategories.UpdateAt
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

        [HttpPut("{id?}")]
        [ActionName("subcategory-update")]
        public async Task<ActionResult<ResponseModel<AdminSubCategoriesUpdateDto>>> Update([FromServices] ResponseModel responseModel, [FromBody] AdminSubCategoriesUpdateDto dto, [Required(ErrorMessage = "Parametre Boş Olamaz")] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.Data = dto;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }

                var subCategories = await unitOfWork.GetRepository<SubCategories>().GetByIdAsync(x => x.Id == Guid.Parse(id), asNoTracking: false);
                if (subCategories is not null)
                {

                    if (subCategories.Id != Guid.Parse(id))
                    {
                        responseModel.Data = dto;
                        responseModel.IsSuccess = false;
                        responseModel.Message = new List<string> { "İşlem Yapılamıyor." };
                        return BadRequest(responseModel);
                    }

                    subCategories.Name = dto.Name.CustomToTitleCase();
                    subCategories.CategoriesId = dto.CategoriesId != null ? Guid.Parse(dto.CategoriesId): subCategories.CategoriesId;
                    subCategories.UpdateUserId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    subCategories.UpdateAt = DateTime.UtcNow;
                    subCategories.IsActive = dto.IsActive;

                    await unitOfWork.GetRepository<SubCategories>().Update(subCategories);

                    var result = await unitOfWork.SaveChangesAsync();

                    if (result > 0)
                    {
                        responseModel.Data = dto;
                        responseModel.IsSuccess = true;
                        responseModel.Message = new List<string> { "Kayıt İşlemi Başarılı" };

                        return Ok(responseModel);
                    }
                }

                responseModel.Data = dto;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Güncelleme İşlemi Başarısız", "Sistem Yöneticisi İle İletişime Geçin" };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = dto;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { ex.Message };
                foreach (var item in ex.Data)
                {
                    responseModel.Message.Add(item.ToString());
                }
                return BadRequest(responseModel);
            }
        }


        [HttpDelete("{id?}")]
        [ActionName("subcategory-delete")]
        public async Task<ActionResult<ResponseModel<AdminSubCategoriesDeleteDto>>> Delete([FromServices] ResponseModel responseModel, [Required(ErrorMessage = "Parametre Boş Olamaz")] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.Data = null;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }

                var subCategories = await unitOfWork.GetRepository<SubCategories>().GetByIdAsync(x => x.Id == Guid.Parse(id), asNoTracking: false);
                if (subCategories is not null)
                {

                    if (subCategories.Id != Guid.Parse(id))
                    {
                        responseModel.Data = new AdminSubCategoriesDeleteDto
                        {
                            Name = subCategories.Name.CustomToTitleCase(),
                            IsDelete = subCategories.IsDeleted,
                            IsActive = subCategories.IsActive,
                        };
                        responseModel.IsSuccess = false;
                        responseModel.Message = new List<string> { "İşlem Yapılamıyor." };
                        return BadRequest(responseModel);
                    }

                    subCategories.DeleteUserId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    subCategories.DeleteAt = DateTime.UtcNow;
                    subCategories.IsDeleted = !subCategories.IsDeleted;
                    subCategories.IsActive = !subCategories.IsActive;


                    await unitOfWork.GetRepository<SubCategories>().Update(subCategories);

                    var result = await unitOfWork.SaveChangesAsync();

                    if (result > 0)
                    {
                        responseModel.Data = new AdminSubCategoriesDeleteDto
                        {
                            Name = subCategories.Name.CustomToTitleCase(),
                            IsActive = subCategories.IsActive,
                            IsDelete = subCategories.IsDeleted
                        };
                        responseModel.IsSuccess = true;
                        responseModel.Message = new List<string> { "Kayıt İşlemi Başarılı" };

                        return Ok(responseModel);
                    }
                }

                responseModel.Data = new AdminSubCategoriesDeleteDto
                {
                    Name = subCategories.Name.CustomToTitleCase(),
                    IsActive = subCategories.IsActive,
                    IsDelete = subCategories.IsDeleted
                };
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Güncelleme İşlemi Başarısız", "Sistem Yöneticisi İle İletişime Geçin" };

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
