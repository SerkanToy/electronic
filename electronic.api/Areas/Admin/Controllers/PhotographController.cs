using electronic.api.Areas.Api.Controllers;
using electronic.Application.Interfaces;
using electronic.Domain.DTOs.PhotographDTOs;
using electronic.Domain.Entities.Employees;
using electronic.Domain.Entities.Employees.Product;
using electronic.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Areas.Admin.Controllers
{
    //[Authorize]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class PhotographController : ApiBaseController
    {
        private readonly IWebHostEnvironment _env;
        private readonly IPhotographUpload photographUpload;
        public PhotographController(IWebHostEnvironment env, IPhotographUpload photographUpload)
        {
            _env = env;
            this.photographUpload = photographUpload;
        }

        [HttpGet]
        [ActionName("photograph-list")]
        public async Task<ActionResult<ResponseModel<AdminPhotographAllDto>>> List([FromServices] ResponseModel responseModel)
        {
            try
            {
                var photograph = await unitOfWork.GetRepository<ProductImages>().GetAllAsync(asNoTracking: false);

                var photographDto = photograph.Select(x => new AdminPhotographAllDto
                {
                    IsMain = x.IsMain,
                    Orderby = x.Orderby,
                    ProductId = x.ProductId.ToString(),
                }).ToList();

                responseModel.Data = photographDto;
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
        [ActionName("photograph-add")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ResponseModel<AdminPhotographAddDto>>> Add([FromServices] ResponseModel responseModel, [FromForm] AdminPhotographAddDto adminPhotographAddDto)
        {
            try
            {
                List<object> results = new List<object>();
                if (adminPhotographAddDto.file == null || adminPhotographAddDto.file.Count == 0)
                {
                    responseModel.Data = adminPhotographAddDto;
                    responseModel.IsSuccess = false;
                    responseModel.Message = new List<string> { "Lütfen bir dosya seçin" };
                    return BadRequest(responseModel);
                }

                var (fileUrls, isSuccess) = await photographUpload.UploadPhotograph(adminPhotographAddDto.file);

                if (!isSuccess)
                {
                    responseModel.Data = adminPhotographAddDto;
                    responseModel.IsSuccess = false;
                    responseModel.Message = fileUrls;
                    return BadRequest(responseModel);
                }

                foreach (var item in fileUrls)
                {
                    await unitOfWork.GetRepository<ProductImages>().AddAsync(new ProductImages
                    {
                        Name = item,
                        IsMain = adminPhotographAddDto.IsMain,
                        Orderby = adminPhotographAddDto.Orderby,
                        ProductId = Guid.Parse(adminPhotographAddDto.ProductId)
                    });

                    var result = await unitOfWork.SaveChangesAsync();
                    results.Add(new { Name = item , IsSuccess = result > 0 ? true : false, Description = result > 0 ? "Kayıt İşlemi Başarılı" : "Kayıt İşlemi Başarısız" });
                }

                responseModel.Data = results;
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { "Genel İşlem Tablosu" };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = "";
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
        [ActionName("photograph-addlist")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ResponseModel<AdminPhotographAddDto>>> AddList([FromServices] ResponseModel responseModel, [FromForm] IEnumerable<AdminPhotographAddDtos> adminPhotographAddDto)
        {
            try
            {
                List<object> results = new List<object>();
                foreach (var item in adminPhotographAddDto)
                {
                    if (item.file == null || item.file.Length == 0)
                    {
                        responseModel.Data = adminPhotographAddDto;
                        responseModel.IsSuccess = false;
                        responseModel.Message = new List<string> { "Lütfen bir dosya seçin" };
                        return BadRequest(responseModel);
                    }
                }

                foreach (var adminPhotograph in adminPhotographAddDto)
                {
                    var (fileUrl, isSuccess) = await photographUpload.UploadPhotograph(adminPhotograph.file);
                    if (!isSuccess)
                    {
                        responseModel.Data = adminPhotographAddDto;
                        responseModel.IsSuccess = false;
                        responseModel.Message = new List<string> { fileUrl };
                        return BadRequest(responseModel);
                    }
                    await unitOfWork.GetRepository<ProductImages>().AddAsync(new ProductImages
                    {
                        Name = fileUrl,
                        IsMain = adminPhotograph.IsMain,
                        Orderby = adminPhotograph.Orderby,
                        ProductId = Guid.Parse(adminPhotograph.ProductId)
                    });
                    var result = await unitOfWork.SaveChangesAsync();
                    results.Add(new { Name = fileUrl, IsSuccess = result > 0 ? true : false, Description = result > 0 ? "Kayıt İşlemi Başarılı" : "Kayıt İşlemi Başarısız" });
                }

               

                responseModel.Data = results;
                responseModel.IsSuccess = true;
                responseModel.Message = new List<string> { "Genel İşlem Tablosu" };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Data = "";
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
