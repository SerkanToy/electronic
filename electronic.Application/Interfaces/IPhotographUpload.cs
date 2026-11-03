using electronic.Domain.DTOs.PhotographDTOs;
using Microsoft.AspNetCore.Http;
namespace electronic.Application.Interfaces
{
    public interface IPhotographUpload
    {
        Task<(List<string> fileUrls, bool isSuccess)> UploadPhotograph(List<IFormFile> files);
        Task<(string fileUrl, bool isSuccess)> UploadPhotograph(IFormFile files);
    }
}
