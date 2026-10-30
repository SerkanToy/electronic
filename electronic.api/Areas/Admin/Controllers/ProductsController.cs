using electronic.api.Areas.Api.Controllers;
using electronic.api.Models;
using electronic.Domain.DTOs.Products;
using electronic.Domain.Entities.Employees.Product;
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
    public class ProductsController : ApiBaseController
    {
        [HttpGet]
        [ActionName("product-list")]
        public async Task<ActionResult<ResponseModel<AdminProductAllDto>>> List([FromServices] ResponseModel responseModel)
        {
            try
            {
                var products = await unitOfWork.GetRepository<Products>().GetAllAsync(asNoTracking: false);

                var productDto = products.Select(x => new AdminProductAllDto
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
        [ActionName("product-add")]
        public async Task<ActionResult<ResponseModel<AdminProductAddDto>>> Add([FromServices] ResponseModel responseModel, [FromBody] AdminProductAddDto newProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    responseModel.Data = newProduct;
                    responseModel.IsSuccess = false;
                    responseModel.Message = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList();
                    return BadRequest(responseModel);
                }

                var isCategory = await unitOfWork.GetRepository<Products>().FindAsync(x => x.Name.ToLower() == newProduct.Name.ToLower(), asNoTracking: false);
                if (isCategory.Count() == 0)
                {
                    await unitOfWork.GetRepository<Products>().AddAsync(new Products
                    {
                        Name = newProduct.Name.CustomToTitleCase(),
                        Description = newProduct.Description,
                        CreateUserId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value) 
                    });

                    var result = await unitOfWork.SaveChangesAsync();

                    if (result > 0)
                    {
                        responseModel.Data = newProduct;
                        responseModel.IsSuccess = true;
                        responseModel.Message = new List<string> { "Kayıt İşlemi Başarılı" };

                        return Ok(responseModel);
                    }
                }

                responseModel.Data = newProduct;
                responseModel.IsSuccess = false;
                responseModel.Message = new List<string> { "Kayıt İşlemi Başarısız", "Ürün Adının Olmadığından Emin Olun", "Sistem Yöneticisi İle İletişime Geçin" };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = newProduct;
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
        [ActionName("product-detail")]
        public async Task<ActionResult<ResponseModel<AdminProductDetailDto>>> Detail([FromServices] ResponseModel responseModel, [Required(ErrorMessage = "Parametre Boş Olamaz")] string id)
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

                var category = await unitOfWork.GetRepository<Products>().GetByIdAsync(x => x.Id == Guid.Parse(id), asNoTracking: false);
                if (category is null)
                {
                    responseModel.Data = null;
                    responseModel.IsSuccess = false;
                    responseModel.Message = new List<string> { "İşlemi Başarısız", "Ürün Bulunamadı." };
                    return Ok(responseModel);
                }


                responseModel.Data = new AdminProductDetailDto
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

        [HttpPut("{id?}")]
        [ActionName("product-update")]
        public async Task<ActionResult<ResponseModel<AdminProductUpdateDto>>> Update([FromServices] ResponseModel responseModel, [FromBody] AdminProductUpdateDto dto, [Required(ErrorMessage = "Parametre Boş Olamaz")] string id)
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

                var isProduct = await unitOfWork.GetRepository<Products>().GetByIdAsync(x => x.Id == Guid.Parse(id), asNoTracking: false);
                if (isProduct is not null)
                {

                    if (isProduct.Id != Guid.Parse(id))
                    {
                        responseModel.Data = dto;
                        responseModel.IsSuccess = false;
                        responseModel.Message = new List<string> { "İşlem Yapılamıyor." };
                        return BadRequest(responseModel);
                    }


                    isProduct.Name = dto.Name.CustomToTitleCase();
                    isProduct.Description = dto.Description;
                    isProduct.icon = dto.icon is null ? isProduct.icon : dto.icon;
                    isProduct.UpdateUserId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    isProduct.UpdateAt = DateTime.UtcNow;
                    isProduct.IsActive = dto.IsActive;



                    await unitOfWork.GetRepository<Products>().Update(isProduct);

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
        [ActionName("product-delete")]
        public async Task<ActionResult<ResponseModel<AdminProductDeleteDto>>> Delete([FromServices] ResponseModel responseModel, [Required(ErrorMessage = "Parametre Boş Olamaz")] string id)
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

                var isProduct = await unitOfWork.GetRepository<Products>().GetByIdAsync(x => x.Id == Guid.Parse(id), asNoTracking: false);
                if (isProduct is not null)
                {

                    if (isProduct.Id != Guid.Parse(id))
                    {
                        responseModel.Data = new AdminProductDeleteDto
                        {
                            Name = isProduct.Name.CustomToTitleCase(),
                            Description = isProduct.Description,
                            IsDelete = isProduct.IsDeleted,
                            IsActive = isProduct.IsActive,
                        };
                        responseModel.IsSuccess = false;
                        responseModel.Message = new List<string> { "İşlem Yapılamıyor." };
                        return BadRequest(responseModel);
                    }

                    isProduct.DeleteUserId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                    isProduct.DeleteAt = DateTime.UtcNow;
                    isProduct.IsDeleted = !isProduct.IsDeleted;
                    isProduct.IsActive = !isProduct.IsActive;


                    await unitOfWork.GetRepository<Products>().Update(isProduct);

                    var result = await unitOfWork.SaveChangesAsync();

                    if (result > 0)
                    {
                        responseModel.Data = new AdminProductDeleteDto
                        {
                            Name = isProduct.Name.CustomToTitleCase(),
                            Description = isProduct.Description,
                            IsDelete = isProduct.IsDeleted
                        };
                        responseModel.IsSuccess = true;
                        responseModel.Message = new List<string> { "Kayıt İşlemi Başarılı" };

                        return Ok(responseModel);
                    }
                }

                responseModel.Data = new AdminProductDeleteDto
                {
                    Name = isProduct.Name.CustomToTitleCase(),
                    Description = isProduct.Description,
                    IsDelete = isProduct.IsDeleted,
                    IsActive = isProduct.IsActive
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
